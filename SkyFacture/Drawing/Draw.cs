// The NiTiS-Dev licenses this file to you under the MIT license.

using OpenTK.Graphics.OpenGL;
using SkyFacture.Collections;
using SkyFacture.Drawing.Shading;
using SkyFacture.Drawing.Sprites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFacture.Drawing;
public static class Draw
{
	public static void Texture(Region2D region, vec3 position, vec2 size, Camera? watcher = null)
	{

	}
	public static class TextureDrawer
	{
		private static int VAO, VBO;
		public const uint VertexCount = 6; // 2 Triangles
		public const uint VertexStride = sizeof(float) * 8;
		public const uint BufferSize = VertexCount * VertexStride;
		private const float v = 0.505f;
		public static void Draw(Texture2D texture, vec3 pos, vec2 size, vec2 uvs, vec2 uve, Color colorLB, Color colorRB, Color colorLT, Color colorRT, Camera? watcher = null)
		{
			float[] position = new float[12]
			{
				-v, -v, //LB
				+v, -v, //RB
				+v, +v, //RT
				-v, -v, //LB
				+v, +v, //RT
				-v, +v, //LT
			};
			float[] texPosition = new float[12]
			{
				uvs.X, uvs.Y,
				uve.X, uvs.Y,
				uve.X, uve.Y,
				uvs.X, uvs.Y,
				uve.X, uve.Y,
				uvs.X, uve.Y,
			};
			GL.BindVertexArray(VAO);
			GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
			texture.Use(TextureUnit.Texture4);

			for (int i = 0; i < VertexCount; i++)
			{
				// Position TODO: Paste it during first initialization
				GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(i * VertexStride), sizeof(float) * 2, new float[2] { position[i * 2], position[i * 2 + 1] });
				// TexPos
				GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(i * VertexStride + sizeof(float) * 2), sizeof(float) * 2, new float[2] { texPosition[i * 2], texPosition[i * 2 + 1] });
			}
			Shaders.DefShader.Use();
			GL.EnableVertexAttribArray(0);
			GL.EnableVertexAttribArray(1);
			GL.EnableVertexAttribArray(2);
			mat4 matrix = mat4.Identity;
			if (watcher is not null) matrix *= watcher.GetTranslation();
			matrix *= mat4.CreateTranslation(pos);
			matrix *= mat4.CreateScale(size.X, size.Y, 1f);
			if (watcher is not null) matrix *= watcher.GetView();
			Shaders.DefShader.Matrix(matrix);
			Shaders.DefShader.Texture(TextureUnit.Texture0);
			GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
		}
		static TextureDrawer()
		{
			VAO = GL.GenVertexArray();
			GL.BindVertexArray(VAO);

			VBO = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
			GL.BufferData(BufferTarget.ArrayBuffer, (int)BufferSize, ArrayFactory.WithValue<float>(VertexCount * 8, 1f), BufferUsageHint.StreamDraw);
		}
	}
}
