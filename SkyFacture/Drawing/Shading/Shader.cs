

using OpenTK.Graphics.OpenGL;
using System;

namespace SkyFacture.Drawing.Shading;
public class Shader : IShader
{
	private readonly int handle;
	public int Handle => this.handle;
	public virtual void SetDefaults() { }
	public Shader(string vertexShader, string fragmentShader)
	{
		int vert, frag;

		vert = GL.CreateShader(ShaderType.VertexShader);

		GL.ShaderSource(vert, vertexShader);
		GL.CompileShader(vert);

		string infoLog = GL.GetShaderInfoLog(vert);
		if (!String.IsNullOrWhiteSpace(infoLog))
			Console.WriteLine($"Error compiling vertex shader {infoLog}");

		frag = GL.CreateShader(ShaderType.FragmentShader);

		GL.ShaderSource(frag, fragmentShader);
		GL.CompileShader(frag);

		infoLog = GL.GetShaderInfoLog(frag);
		if (!String.IsNullOrWhiteSpace(infoLog))
			Console.WriteLine($"Error compiling vertex shader {infoLog}");

		this.handle = GL.CreateProgram();
		GL.AttachShader(this.handle, vert);
		GL.AttachShader(this.handle, frag);
		GL.LinkProgram(this.handle);

		GL.GetProgram(this.handle, GetProgramParameterName.LinkStatus, out int status);
		if (status == 0)
			Console.WriteLine($"Error linking shader {GL.GetProgramInfoLog(this.handle)}");

		GL.DetachShader(this.handle, vert);
		GL.DetachShader(this.handle, frag);
		GL.DeleteShader(vert);
		GL.DeleteShader(frag);
	}
	protected int UniformPosition(string uniformName)
		=> GL.GetUniformLocation(Handle, uniformName);
	public void Bind()
		=> GL.UseProgram(this.handle);
	public virtual void Use()
	{
		Bind();
		Shaders.Current = this;
	}
	public virtual void Dispose()
		=> GL.DeleteProgram(this.handle);
}
