
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.IO;

namespace SkyFacture.Drawing.Shading;
public class DefaultShader : Shader
{
	private readonly int matrix, tex;
	public DefaultShader()
		: base(File.ReadAllText("GameContent/Shaders/default.vert")
			, File.ReadAllText("GameContent/Shaders/default.frag"))
	{
		const int stride = (8 * sizeof(float));

		// Position
		GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, stride, IntPtr.Zero);
		//GL.EnableVertexAttribArray(0);

		// Tex-position
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, stride, 2 * sizeof(float));
		//GL.EnableVertexAttribArray(1);

		// Color :|
		GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, stride, 4 * sizeof(float));
		//GL.EnableVertexAttribArray(2);

		this.matrix = GL.GetUniformLocation(Handle, "matrix");
		this.tex = GL.GetUniformLocation(Handle, "tex");
	}
	public override void SetDefaults()
	{
		Texture(TextureUnit.Texture0);
		Matrix(mat4.Identity);
	}
	public void Texture(TextureUnit unit = TextureUnit.Texture0)
		=> GL.Uniform1(this.tex, (int)(unit - TextureUnit.Texture0)); // IMPORTANT TO SUBSTRACT Texture0 from unit, to cast Texture0 to 0 and Texture1 to 1 (not 33985)
	public void Matrix(mat4 matrix)
		=> GL.UniformMatrix4(this.matrix, false, ref matrix);
}
