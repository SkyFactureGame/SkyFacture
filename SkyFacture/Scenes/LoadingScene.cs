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
		//EBO = new(BufferTargetARB.ElementArrayBuffer);
		//EBO.Bind();
		//EBO.Data(Index);

		//VBO = new(BufferTargetARB.ArrayBuffer);
		//VBO.Bind();
		//VBO.Data(Vert);

		//VAO = new();
		//VAO.Bind();

		//EBO.Bind();

		//VAO.AttributePointer(0, 3, VertexAttribPointerType.Float, 5 * sizeof(float), 0);
		//VAO.AttributePointer(1, 2, VertexAttribPointerType.Float, 5 * sizeof(float), 3 * sizeof(float));
	}
	public override void Dispose()
	{
	}
	public override void Render(double delta)
	{
		Gl.ClearColor(Color.Black);
		Gl.Clear(ClearBufferMask.ColorBufferBit);

		sb.Draw((SpriteRegion)sprite, default, vec2.One);
		sb.ZLayer(0f);
		sb.Draw((SpriteRegion)sprite, new(0.25f, 0.25f), vec2.One);
		sb.ZLayer(1f);
		sb.Flush();
	}
	public override void Update(double delta)
	{
	}
}
