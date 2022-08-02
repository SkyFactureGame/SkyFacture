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
	public ShaderProgram(GL gl, string vertexCode, string fragmentCode)
	{
		uint vert, frag;
		vert = LoadShader(gl, ShaderType.VertexShader, vertexCode);
		frag = LoadShader(gl, ShaderType.FragmentShader, fragmentCode);

		Handle = gl.CreateProgram();

		gl.AttachShader(Handle, vert);
		gl.AttachShader(Handle, frag);
		gl.LinkProgram(Handle);
		gl.GetProgram(Handle, ProgramPropertyARB.LinkStatus, out int status);
		if (status == 0)
			throw new Exception($"ShaderProgram failed to link with error: {gl.GetProgramInfoLog(Handle)}");
		gl.DetachShader(Handle, vert);
		gl.DetachShader(Handle, frag);
		gl.DeleteShader(vert);
		gl.DeleteShader(frag);

		gl.GetProgram(Handle, ProgramPropertyARB.ActiveUniforms, out int numberOfUniforms);
		uniforms = new Dictionary<string, int>(numberOfUniforms);
		for (uint i = 0; i < numberOfUniforms; i++)
			uniforms[gl.GetActiveUniform(Handle, i, out _, out _)] = (int)i;
	}
	public void Use(GL gl)
		=> gl.UseProgram(Handle);
	public void UniformInt32(GL gl, string name, int value)
		=> gl.Uniform1(uniforms[name], value);
	public void UniformF32(GL gl, string name, float value)
		=> gl.Uniform1(uniforms[name], value);
	public void UniformF64(GL gl, string name, double value)
		=> gl.Uniform1(uniforms[name], value);
	public void UniformVec2(GL gl, string name, Vector2D<float> value)
		=> gl.Uniform2(uniforms[name], value.X, value.Y);
	public void UniformVec2i(GL gl, string name, Vector2D<int> value)
		=> gl.Uniform2(uniforms[name], value.X, value.Y);
	public void UniformVec3(GL gl, string name, Vector3D<float> value)
		=> gl.Uniform3(uniforms[name], value.X, value.Y, value.Z);
	public void UniformVec3i(GL gl, string name, Vector3D<int> value)
		=> gl.Uniform3(uniforms[name], value.X, value.Y, value.Z);
	public void UniformColor(GL gl, string name, Color color)
		=> gl.Uniform4(uniforms[name], color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
	public void UniformVec4(GL gl, string name, Vector4D<float> value)
		=> gl.Uniform4(uniforms[name], value.X, value.Y, value.Z, value.W);
	public void UniformVec4i(GL gl, string name, Vector4D<int> value)
		=> gl.Uniform4(uniforms[name], value.X, value.Y, value.Z, value.W);
	private static uint LoadShader(GL gl, ShaderType type, string code)
	{
		uint handle = gl.CreateShader(type);
		gl.ShaderSource(handle, code);
		gl.CompileShader(handle);
		string infoLog = gl.GetShaderInfoLog(handle);
		if (!String.IsNullOrWhiteSpace(infoLog))
			throw new Exception($"Error compiling shader of type: {type}\nInfo: {infoLog}");

		return handle;
	}
}
