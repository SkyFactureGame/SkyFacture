using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;

namespace SkyFacture.Drawing.Sprites;
public class Texture2D : GLObj
{
	public readonly int Width, Height;
	public int Handle => this.handle;
	public Texture2D(Stream stream, bool blending = true) : this(Image.Load<Rgba32>(stream), blending) { }
	public Texture2D(byte[] bytes, bool blending = true) : this(Image.Load<Rgba32>(bytes), blending) { }
	public Texture2D(Image<Rgba32> image, bool blending = true) : base(GL.GenTexture())
	{
		byte[] pixels = new byte[4 * image.Width * image.Height];
		uint pixelN = 0;
		for (int y = 0; y < image.Height; y++)
			for (int x = 0; x < image.Width; x++)
			{
				Rgba32 pixel = image[x, image.Height - y - 1];
				pixels[pixelN * 4 + 0] = pixel.R;
				pixels[pixelN * 4 + 1] = pixel.G;
				pixels[pixelN * 4 + 2] = pixel.B;
				pixels[pixelN * 4 + 3] = pixel.A;
				pixelN++;
			}

		this.Width = image.Width;
		this.Height = image.Height;

		GL.BindTexture(TextureTarget.Texture2D, this.handle);
		GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);

		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.MirroredRepeat);
		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.MirroredRepeat);

		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)(blending ? TextureMinFilter.Linear : TextureMinFilter.Nearest));
		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)(blending ? TextureMagFilter.Linear : TextureMagFilter.Nearest));

		GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
	}
	public void Use(TextureUnit unit = TextureUnit.Texture0)
	{
		GL.ActiveTexture(unit);
		GL.BindTexture(TextureTarget.Texture2D, this.handle);
	}
	public void Use(int unit)
	{
		GL.ActiveTexture(TextureUnit.Texture0 + unit);
		GL.BindTexture(TextureTarget.Texture2D, this.handle);
	}
	public void Bind()
		=> GL.BindTexture(TextureTarget.Texture2D, this.handle);
	public void Dispose()
		=> GL.DeleteTexture(this.handle);
}
