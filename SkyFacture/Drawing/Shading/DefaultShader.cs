
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.IO;

namespace SkyFacture.Drawing.Shading;
public class DefaultShader : Shader
{
	private const int stride = (4 * sizeof(float));
	private readonly int matrix, tex, color;
	public DefaultShader()
		: base(File.ReadAllText("GameContent/Shaders/default.vert")
			, File.ReadAllText("GameContent/Shaders/default.frag")
			, new Attribute[] 
			{
				new(0, VertexAttribPointerType.Float, 2, false, stride, 0),
				new(1, VertexAttribPointerType.Float, 2, false, stride, 2 * sizeof(float))
			})
	{
		this.matrix = GL.GetUniformLocation(handle, "matrix");
		this.tex = GL.GetUniformLocation(handle, "tex");
	}
	public override Attribute[] VertexAttributes()
		=> base.VertexAttributes();
	public override void SetDefaults()
	{
		Texture(TextureUnit.Texture0);
		Matrix(mat4.Identity);
	}
	public void Texture(ushort unit = 0)
		=> GL.Uniform1(this.tex, (int)unit);
	public void Texture(TextureUnit unit = TextureUnit.Texture0)
		=> GL.Uniform1(this.tex, (int)(unit - TextureUnit.Texture0)); // IMPORTANT TO SUBSTRACT Texture0 from unit, to cast Texture0 to 0 and Texture1 to 1 (not 33985)
	public void Matrix(mat4 matrix)
		=> GL.UniformMatrix4(this.matrix, false, ref matrix);
	public void Color(vec4 color)
		=> GL.Uniform4(this.color, color);
	public void Color(Color color)
		=> GL.Uniform4(this.color, color);
}
