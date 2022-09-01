using Silk.NET.OpenGL;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System;
using System.IO;

namespace SkyFacture.Graphics.Textures;

public class Sprite : IDisposable
{
	public readonly uint Handle;
	public readonly uint Width, Height;

	public unsafe Sprite(Stream imageStream)
	{
		Handle = Gl.GenTexture();

		Gl.BindTexture(TextureTarget.Texture2D, Handle);
		//SL.InitializeData(this, imageStream, out Width, out Height);
		using Image<Rgba32> image = Image.Load<Rgba32>(imageStream);

		byte[] pixels = new byte[4 * image.Width * image.Height];
		fixed (void* pixelsPtr = pixels)
		{
			Rgba32* ptr = (Rgba32*)pixelsPtr;
			uint pixelN = 0;
			for (int y = 0; y < image.Height; y++)
				for (int x = 0; x < image.Width; x++)
				{
					Rgba32 pixel = image[x, image.Height - y - 1];
					ptr[pixelN] = pixel;
					pixelN++;
				}
			
			Gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)image.Width, (uint)image.Height, 0,
			PixelFormat.Bgra, PixelType.UnsignedByte, ptr);
		}

		Width = (uint)image.Width;
		Height = (uint)image.Height;
		SetParameters();
	}
	private void SetParameters(bool blending = false)
	{
		Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
		Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
		Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.LinearMipmapLinear);
		Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);
		Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
		Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 8);
		Gl.GenerateMipmap(TextureTarget.Texture2D);
	}
	public void Bind(TextureUnit unit = TextureUnit.Texture0)
	{
		Gl.ActiveTexture(unit);
		Gl.BindTexture(TextureTarget.Texture2D, Handle);
	}
	public void Dispose()
		=> Gl.DeleteTexture(Handle);

	public static explicit operator Region(Sprite sprite)
		=> new(sprite, new vec2(0, 0), new vec2(1, 1));
}
