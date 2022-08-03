using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace SkyFacture.Graphics.Shaders;

public readonly struct ShaderProgram
{
	public readonly uint Handle;
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
	}
	public void Use()
		=> Gl.UseProgram(Handle);
	public void UniformInt(string name, int value)
		=> Gl.Uniform1(Gl.GetUniformLocation(Handle, name), value);
	public void UniformSingle(string name, float value)
		=> Gl.Uniform1(Gl.GetUniformLocation(Handle, name), value);
	public unsafe void UniformMat4<T>(string name, Matrix4X4<float> mat4)
		=> Gl.UniformMatrix4(Gl.GetUniformLocation(Handle, name), 1, false, (float*)&mat4);
	private static uint LoadShader(ShaderType type, string code)
	{
		uint handle = Gl.CreateShader(type);
		Gl.ShaderSource(handle, code);
		Gl.CompileShader(handle);
		string infoLog = Gl.GetShaderInfoLog(handle);
		if (!String.IsNullOrWhiteSpace(infoLog))
			throw new Exception($"Error compiling shader of type: {type}\nInfo: {infoLog}");

		//Console.WriteLine($"{type}:\n{code}");

		return handle;
	}
}
