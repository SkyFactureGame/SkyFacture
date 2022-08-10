using Silk.NET.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SkyFacture.Graphics.Textures;
using System;
using System.Diagnostics;
using System.IO;

namespace SkyFacture.Windows;

//TODO: Use default library to icrease loading speed on windows
public class WindowsSpriteLoader : SpriteLoader
{
	public override unsafe void InitializeData(Sprite sprite, Stream imageStream, out uint width, out uint height)
	{
		Stopwatch sw = new();
		sw.Start();
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

			sw.Stop();

			// 90~160ms
			Console.WriteLine($"Texture loading: {sw.ElapsedMilliseconds}ms");

			Core.Gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)image.Width, (uint)image.Height, 0,
			PixelFormat.Bgra, PixelType.UnsignedByte, ptr);
		}

		width = (uint)image.Width;
		height = (uint)image.Height;
	}
}
