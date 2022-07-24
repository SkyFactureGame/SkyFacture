using SkyFacture.Drawing.Buffers;
using SkyFacture.Drawing.Shading;

namespace SkyFacture.Drawing;
public static partial class Draw
{
	public static class Texture
	{
		private static readonly VertexArray VAO;
		private static readonly Buffer<float> VBO;
		static Texture()
		{
			VAO = new(Shaders.DefShader);
			VBO = new(OpenTK.Graphics.OpenGL.BufferTarget.ArrayBuffer, sizeof(float) * 4);
		}
	}
}
