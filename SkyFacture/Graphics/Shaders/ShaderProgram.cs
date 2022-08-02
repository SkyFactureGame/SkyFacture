using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SkyFacture.Graphics.Shaders;

public readonly struct ShaderProgram
{
	public readonly uint Handle;
	private readonly Dictionary<string, int> uniforms;
	public ShaderProgram(string vertexCode, string fragmentCode)
	{
		uint vert, frag;
		vert = LoadShader(ShaderType.VertexShader, vertexCode);
		frag = LoadShader(ShaderType.FragmentShader, fragmentCode);

		Handle = Gl.CreateProgram();

		Gl.AttachShader(Handle, vert);
		Gl.AttachShader(Handle, frag);
		Gl.LinkProgram(Handle);
		Gl.GetProgram(Handle, ProgramPropertyARB.LinkStatus, out int status);
		if (status == 0)
			throw new Exception($"ShaderProgram failed to link with error: {Gl.GetProgramInfoLog(Handle)}");
		Gl.DetachShader(Handle, vert);
		Gl.DetachShader(Handle, frag);
		Gl.DeleteShader(vert);
		Gl.DeleteShader(frag);

		Gl.GetProgram(Handle, ProgramPropertyARB.ActiveUniforms, out int numberOfUniforms);
		uniforms = new Dictionary<string, int>(numberOfUniforms);
		for (uint i = 0; i < numberOfUniforms; i++)
			uniforms[Gl.GetActiveUniform(Handle, i, out _, out _)] = (int)i;
	}
	public void Use()
		=> Gl.UseProgram(Handle);
	private static uint LoadShader(ShaderType type, string code)
	{
		uint handle = Gl.CreateShader(type);
		Gl.ShaderSource(handle, code);
		Gl.CompileShader(handle);
		string infoLog = Gl.GetShaderInfoLog(handle);
		if (!String.IsNullOrWhiteSpace(infoLog))
			throw new Exception($"Error compiling shader of type: {type}\nInfo: {infoLog}");

		return handle;
	}
}
