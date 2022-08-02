using Silk.NET.OpenGL;
using SkyFacture.Graphics.Textures;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SkyFacture.Windows;
public class WindowsSpriteLoader : SpriteLoader
{
	public override void InitializeData(Sprite sprite, Stream imageStream)
	{
		using Bitmap bitmap = new(imageStream);

		BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

		Core.Gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba8, (uint)bitmap.Width, (uint)bitmap.Height, 0, Silk.NET.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, data.Scan0);

		bitmap.UnlockBits(data);
	}
}
