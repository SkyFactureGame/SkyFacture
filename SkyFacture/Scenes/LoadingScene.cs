using Silk.NET.OpenGL;
using SkyFacture.Content.Drawing;
using SkyFacture.Graphics.Batching;
using SkyFacture.Graphics.Memory;
using SkyFacture.Graphics.Shaders;
using SkyFacture.Graphics.Textures;
using System.Drawing;
using System.IO;

namespace SkyFacture.Scenes;
public unsafe class LoadingScene : Scene
{
	private readonly SpriteBatcher sb = new(Shaders.Default);
	private Sprite sprite;
	private readonly float[] Vert = new float[]
	{
		//X    Y      Z     U   V
		 0.5f,  0.5f, 0.0f, 1f, 0f, // 0
		 0.5f, -0.5f, 0.0f, 1f, 1f, // 1
		-0.5f, -0.5f, 0.0f, 0f, 1f, // 2
		-0.5f,  0.5f, 0.5f, 0f, 0f, // 3
	};
	private readonly uint[] Index = new uint[]
	{
		0, 1, 3,
		1, 2, 3
	};
	private float progress = -1f;
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

	}
	public override void Update(double delta)
	{
		progress += (float)delta;
		if (progress > 1f)
		{
			progress = -1f;
		}
	}
}
