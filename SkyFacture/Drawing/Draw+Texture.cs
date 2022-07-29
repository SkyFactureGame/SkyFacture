using OpenTK.Graphics.OpenGL;
using SkyFacture.Drawing.Buffers;
using SkyFacture.Drawing.Shading;
using SkyFacture.Drawing.Sprites;
using System;
using System.Diagnostics;

namespace SkyFacture.Drawing;
public static partial class Draw
{
	public const float TextureUnitSize = 2f;
	public static class Texture
	{
		public static readonly Stopwatch SW;
		private static readonly VertexArray VAO;
		private static readonly Buffer<float> VBO;
		static Texture()
		{
			VAO = new(2);
			VBO = new(BufferTarget.ArrayBuffer, sizeof(float) * 2);
			VBO.Init(12 * sizeof(float), 6, new float[12]
			{
				-0.5f, -0.5f,
				 0.5f, -0.5f,
				 0.5f,  0.5f,
				 0.5f,  0.5f,
				-0.5f,  0.5f,
				-0.5f, -0.5f,
			}, BufferUsageHint.StaticDraw);
			VAO.BindAttribute(VBO.CreateAttribute(Shaders.DefShader.vPos, VertexAttribPointerType.Float), VBO);

			SW = new();
		}
		public static void Region(Region2D region, quat rotation, vec2 position, vec2 size, Camera watcher, bool ui = false)
		{
			SW.Start();
			VAO.Bind();
			Shaders.DefShader.Bind();
			VAO.BindAttribute(region.UV.CreateAttribute(Shaders.DefShader.vUV, VertexAttribPointerType.Float), region.UV);
			region.Use(TextureUnit.Texture6);

			mat4 ident = mat4.Identity;
			if (!ui) ident *= watcher.GetTranslation();
			ident *= mat4.CreateScale(TextureUnitSize * size.X, TextureUnitSize * size.Y, 1f);
			ident *= mat4.CreateFromQuaternion(rotation);
			ident *= mat4.CreateTranslation(new vec3(position.X * TextureUnitSize, position.Y * TextureUnitSize, 0));
			ident *= mat4.CreateScale(watcher.Scale, watcher.Scale, 1f);
			ident *= watcher.GetOrthoProj();

			Shaders.DefShader.Texture(TextureUnit.Texture6);
			Shaders.DefShader.ZLayer(zLayer);
			Shaders.DefShader.Matrix(ident);

			VAO.Draw(PrimitiveType.Triangles, 0, 6);
			SW.Stop();
		}
		public static void RegionSeq(Region2D region, Func<bool> drawer, Camera watcher, bool ui = false)
		{
			SW.Start();
			region.Use(TextureUnit.Texture4);
			while (drawer())
			{
				VAO.Draw(PrimitiveType.Triangles, 0, 6);
			}
			SW.Stop();
		}
	}
}
