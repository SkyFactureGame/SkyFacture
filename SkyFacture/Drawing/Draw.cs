using System.Drawing;
using SkyFacture.Drawing.Shading;
using SkyFacture.Drawing.Sprites;

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
	public static void Region(Region2D region, Camera watcher, bool ui = false)
		=> Texture.Region(region, quat.Identity, default, vec2.One, watcher, ui);
	public static void Region(Region2D region, vec2 pos, Camera watcher, bool ui = false)
		=> Texture.Region(region, quat.Identity, pos, vec2.One, watcher, ui);
	public static void Region(Region2D region, vec2 pos, vec2 size, Camera watcher, bool ui = false)
		=> Texture.Region(region, quat.Identity, pos, size, watcher, ui);
	public static void Region(Region2D region, quat rotation, vec2 pos, vec2 size, Camera watcher, bool ui = false)
		=> Texture.Region(region, rotation, pos, size, watcher, ui);
}
