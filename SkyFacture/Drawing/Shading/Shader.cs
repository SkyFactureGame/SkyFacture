using OpenTK.Graphics.OpenGL;
using System;

namespace SkyFacture.Drawing.Shading;

public class Shader : GLObj
{
	public virtual void SetDefaults() { }
	public Shader(string vertexShader, string fragmentShader) : base(GL.CreateProgram())
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

		GL.AttachShader(this.Handle, vert);
		GL.AttachShader(this.Handle, frag);
		GL.LinkProgram(this.Handle);

		GL.GetProgram(this.Handle, GetProgramParameterName.LinkStatus, out int status);
		if (status == 0)
			Console.WriteLine($"Error linking shader {GL.GetProgramInfoLog(this.Handle)}");

		GL.DetachShader(this.Handle, vert);
		GL.DetachShader(this.Handle, frag);
		GL.DeleteShader(vert);
		GL.DeleteShader(frag);
	}
	protected int UniformPosition(string uniformName)
		=> GL.GetUniformLocation(Handle, uniformName);
	public void Bind()
	{
		if (BindValidator.ShoudBind(BindTarget.Shader, Handle))
			GL.UseProgram(this.Handle);
	}
	public virtual void Dispose()
		=> GL.DeleteProgram(this.Handle);
	~Shader()
		=> Dispose();
}
