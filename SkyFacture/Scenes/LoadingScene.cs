using Silk.NET.OpenGL;
using SkyFacture.Graphics.Memory;
using SkyFacture.Graphics.Shaders;
using SkyFacture.Graphics.Textures;
using System;
using System.Drawing;
using System.IO;

namespace SkyFacture.Scenes;
public unsafe class LoadingScene : Scene
{
	private ShaderProgram shader;
	private Sprite sprite;
	private Buffer<uint> EBO;
	private Buffer<float> VBO;
	private Graphics.Memory.VertexArray VAO;
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
	private float progress = 0.15f;
	public override void Initialize()
	{
		shader = new(Core.FM.InternalRead("SkyFacture/Content/Shaders/default.vert"), Core.FM.InternalRead("SkyFacture/Content/Shaders/default.frag"));
		using (Stream spriteStream = Core.FM.Internal("SkyFacture/Content/Sprites/Special/debug.png").OpenForRead())
		{
			sprite = new(spriteStream);
		}
		EBO = new(BufferTargetARB.ElementArrayBuffer);
		EBO.Bind();
		EBO.Data(Index);

		VBO = new(BufferTargetARB.ArrayBuffer);
		VBO.Bind();
		VBO.Data(Vert);

		VAO = new();
		VAO.Bind();

		EBO.Bind();

		VAO.AttributePointer(0, 3, VertexAttribPointerType.Float, 5 * sizeof(float), 0);
		VAO.AttributePointer(1, 2, VertexAttribPointerType.Float, 5 * sizeof(float), 3 * sizeof(float));
	}
	public override void Dispose()
	{

	}
	public override void Render()
	{
		Gl.ClearColor(Color.Black);
		Gl.Clear(ClearBufferMask.ColorBufferBit);

		VAO.Bind();
		shader.Use();

		VBO.SubData(0, (nuint)(sizeof(float) * Vert.Length), new float[]
		{
			//X    Y      Z     U   V
			 progress,  0.5f, 0.0f, 1f, 0f, // 0
			 progress, -0.5f, 0.0f, 1f, 1f, // 1
			-1f, -0.5f, 0.0f, 0f, 1f, // 2
			-1f,  0.5f, 0.5f, 0f, 0f, // 3
		});

		sprite.Bind(TextureUnit.Texture0);
		shader.UniformInt("uTex", 0);
		Gl.DrawElements(PrimitiveType.Triangles, (uint)Index.Length, DrawElementsType.UnsignedInt, null);
	}
	public override void Update()
	{
		progress += 0.015f;
		if (progress > 1f)
		{
			progress = -1f;
		}
	}
}
