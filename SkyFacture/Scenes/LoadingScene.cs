using Silk.NET.OpenGL;
using SkyFacture.Content.Drawing;
using SkyFacture.Graphics.Batching;
using SkyFacture.Graphics.Memory;
using SkyFacture.Graphics.Shaders;
using SkyFacture.Graphics.Textures;
using System;
using System.Drawing;
using System.IO;

namespace SkyFacture.Scenes;
public unsafe class LoadingScene : Scene
{
	private readonly SpriteBatcher sb = new(Shaders.Default);
	private Sprite sprite;
	public override void Initialize()
	{
		using (Stream spriteStream = FM.Internal("debug.png").OpenForRead())
		{
			sprite = new(spriteStream);
		}
	}
	public override void Dispose()
	{
		sb.Dispose();
	}
	public override void Render(double delta)
	{
		sb.Draw((SpriteRegion)sprite, default, vec2.One);
		sb.Draw((SpriteRegion)sprite, new(0.25f, 0.25f), vec2.One);

		sb.Flush();
	}
	private double f = 0f;
	public override void Update(double delta)
	{
		f += delta;
		if (f >= 12f)
		{
			SM.ChangeScene(new MainMenuScene());
			f = 0;
			SC.WriteLine("W");
		}
	}
}
