using System.Drawing;
using SkyFacture.Drawing.Shading;

namespace SkyFacture.Drawing;
public static partial class Draw
{
	private static Color lastColor = Palette.White;
	private static int zLayer = 0;
	public static void ZLayer(Layer newLayer)
		=> zLayer = (int)newLayer;
	public static void ZLayer(int newLayer)
		=> zLayer = newLayer;
	public static void Color(Color color)
	{
		lastColor = color;
		Shaders.DefShader.Color(color);
	}
	public static void Alpha(float alpha)
		=> Color(System.Drawing.Color.FromArgb((int)(alpha * 255), lastColor));
}
