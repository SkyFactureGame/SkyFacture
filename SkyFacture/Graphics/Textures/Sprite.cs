using Silk.NET.OpenGL;
using System;
using System.IO;

namespace SkyFacture.Graphics.Textures;

public class Sprite : IDisposable
{
	public readonly uint Handle;
	public readonly int Width, Height;

	public Sprite(Stream imageStream)
	{
		Handle = Gl.GenTexture();

		Gl.BindTexture(TextureTarget.Texture2D, Handle);
		SL.InitializeData(this, imageStream);
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
}
