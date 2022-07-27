using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.IO;

namespace SkyFacture.Drawing.Shading;
public class DefaultShader : Shader
{
	private readonly int matrix, tex, color, zLayer;
	public readonly int vPos, vUV;
	public DefaultShader()
		: base(File.ReadAllText("GameContent/Shaders/default.vert")
			, File.ReadAllText("GameContent/Shaders/default.frag"))
	{
		this.matrix = GL.GetUniformLocation(handle, nameof(matrix));
		this.tex = GL.GetUniformLocation(handle, nameof(tex));
		this.color = GL.GetUniformLocation(handle, nameof(color));

		this.vPos = GL.GetAttribLocation(handle, nameof(vPos));
		this.vUV = GL.GetAttribLocation(handle, nameof(vUV));
	}
	public override void SetDefaults()
	{
		Texture(TextureUnit.Texture0);
		Matrix(mat4.Identity);
	}
	public void Texture(ushort unit = 0)
		=> GL.Uniform1(this.tex, (int)unit);
	public void Texture(TextureUnit unit = TextureUnit.Texture0)
		=> GL.Uniform1(this.tex, (int)(unit - TextureUnit.Texture0));
	public void Matrix(mat4 matrix)
		=> GL.UniformMatrix4(this.matrix, false, ref matrix);
	public void Color(vec4 color)
		=> GL.Uniform4(this.color, color);
	public void Color(Color color)
		=> GL.Uniform4(this.color, color);
	public void ZLayer(float layer)
		=> GL.Uniform1(this.zLayer, layer);
}
