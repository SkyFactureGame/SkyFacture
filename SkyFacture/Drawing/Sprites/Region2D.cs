

using OpenTK.Graphics.OpenGL;
using SkyFacture.Drawing.Buffers;

namespace SkyFacture.Drawing.Sprites;
public class Region2D
{
	public readonly Texture2D Texture;
	public readonly Buffer<float> UV;
	public Region2D(Texture2D texture, vec2 lowLeftPoint, vec2 rightTopPoint)
	{
		this.Texture = texture;
		UV = new(BufferTarget.ArrayBuffer, sizeof(float) * 2);
		UV.Init(12 * sizeof(float), 6, new float[12]
		{
			lowLeftPoint.X, lowLeftPoint.Y,
			rightTopPoint.X, lowLeftPoint.Y,
			rightTopPoint.X, rightTopPoint.Y,
			rightTopPoint.X, rightTopPoint.Y,
			lowLeftPoint.X, rightTopPoint.Y,
			lowLeftPoint.X, lowLeftPoint.Y,
		});
	}
	public void Use(TextureUnit unit = TextureUnit.Texture0)
		=> this.Texture.Use(unit);
	public void Use(int unit = 0)
		=> this.Texture.Use(unit);
}
