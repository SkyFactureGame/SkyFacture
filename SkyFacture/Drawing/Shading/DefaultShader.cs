// The NiTiS-Dev licenses this file to you under the MIT license.
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.IO;

namespace SkyFacture.Drawing.Shading;
public class DefaultShader : Shader
{
	private readonly int color, matrix, tex;
	public DefaultShader() 
		: base(File.ReadAllText("GameContent/Shaders/default.vert")
			,  File.ReadAllText("GameContent/Shaders/default.frag"))
	{
		// Position
		GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), IntPtr.Zero);
		GL.EnableVertexAttribArray(0);

		// Tex-position
		GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
		GL.EnableVertexAttribArray(1);

		color = GL.GetUniformLocation(Handle, "color");
		matrix = GL.GetUniformLocation(Handle, "matrix");
		tex = GL.GetUniformLocation(Handle, "tex");
	}
	public override void SetDefaults()
	{
		Texture(TextureUnit.Texture0);
		Color(Palette.White);
		Matrix(mat4.Identity);
	}
	public void Texture(TextureUnit unit = TextureUnit.Texture0)
		=> GL.Uniform1(tex, (int)(unit - TextureUnit.Texture0)); // IMPORTANT TO SUBSTRACT Texture0 from unit, to cast Texture0 to 0 and Texture1 to 1 (not 33985)
	public void Color(Color color)
		=> Color(new vec4(color.R / Byte.MaxValue, color.G / Byte.MaxValue, color.B / Byte.MaxValue, color.A / Byte.MaxValue));
	public void Color(vec4 color)
		=> GL.Uniform4(this.color, color);
	public void Matrix(mat4 matrix)
		=> GL.UniformMatrix4(this.matrix, false, ref matrix);
}
