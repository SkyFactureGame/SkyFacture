using SkyFacture.Graphics.Memory;
using SkyFacture.Graphics.Shaders;
using SkyFacture.Graphics.Textures;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SkyFacture.Graphics.Batching;
public class SpriteBatcher
{
	private readonly ShaderProgram shader;
	private readonly List<DrawRequest> requests = new((int)MaxRequests);
	private const uint MaxRequests = 128;
	private const uint VertexSize = sizeof(float) * (2 + 3 + 4);
	public uint RequestCount => (uint)requests.Count;
	public bool InvertedZSort { get; set; } = true;

	private Buffer<float> VBO;
	private VertexArray VAO;

	public unsafe SpriteBatcher(ShaderProgram shader)
	{
		this.shader = shader;

		VBO = new(Silk.NET.OpenGL.BufferTargetARB.ArrayBuffer);
		VBO.Bind();
		VBO.DataPtr(VertexSize * MaxRequests * 6, (void*)null);

		VAO = new();
		VAO.Bind();
		VAO.AttributePointer(0, 3, Silk.NET.OpenGL.VertexAttribPointerType.Float, VertexSize, 0);
		VAO.AttributePointer(1, 2, Silk.NET.OpenGL.VertexAttribPointerType.Float, VertexSize, sizeof(float) * 3);
		VAO.AttributePointer(2, 4, Silk.NET.OpenGL.VertexAttribPointerType.Float, VertexSize, sizeof(float) * (3 + 2));
	}
	public void Dispose()
	{
		requests.Clear();

		VAO.Dispose();
		VBO.Dispose();
	}
	public void Draw(SpriteRegion region, vec2 pos, vec2 size)
	{
		requests.Add(new(region, new(pos, lastZ), size, default, vec4.One));
		if (RequestCount >= MaxRequests -1)
		{
			Flush();
		}
	}
	private float lastZ = -1f;
	public void ZLayer(float z)
	{
		lastZ = InvertedZSort ? -z : z;
	}
	public void Flush()
	{
		if (RequestCount == 0)
			return;

		shader.Use();
		shader.UniformMat4("uMat", mat4.Identity);

		for (uint i = 0; i < RequestCount; i++)
		{
			DrawRequest req = requests[(int)i];
			uint offset = i * VertexSize * 6;

			vec3 pos = req.pos;
			vec4 uv = new(req.sprite.Begin.X, req.sprite.Begin.Y, req.sprite.End.X, req.sprite.End.Y);
			vec4 color = req.color;

			VBO.SubData((nint)offset, new float[]
			{
				pos.X - 0.5f, pos.Y - 0.5f, pos.Z,
				uv.X, uv.Y,
				color.X, color.Y, color.Z, color.W,

				pos.X + 0.5f, pos.Y - 0.5f, pos.Z,
				uv.Z, uv.Y,
				color.X, color.Y, color.Z, color.W,

				pos.X + 0.5f, pos.Y + 0.5f, pos.Z,
				uv.Z, uv.W,
				color.X, color.Y, color.Z, color.W,

				pos.X - 0.5f, pos.Y - 0.5f, pos.Z,
				uv.X, uv.Y,
				color.X, color.Y, color.Z, color.W,

				pos.X + 0.5f, pos.Y + 0.5f, pos.Z,
				uv.Z, uv.W,
				color.X, color.Y, color.Z, color.W,

				pos.X - 0.5f, pos.Y + 0.5f, pos.Z,
				uv.X, uv.W,
				color.X, color.Y, color.Z, color.W,
			});
		}

		VAO.Bind();

		Gl.DrawArrays(Silk.NET.OpenGL.PrimitiveType.Triangles, 0, 6 * RequestCount);

		requests.Clear();
	}
}