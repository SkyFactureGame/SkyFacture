using System.Drawing;

namespace SkyFacture;
public static class SkyMath
{
	public static int Lerp(int from, int to, float progress)
		=> (int)(from + (to - from) * progress);
	public static float Lerp(float from, float to, float progress)
		=> from + (to - from) * progress;
	public static double Lerp(double from, double to, double progress)
		=> from + (to - from) * progress;
	public static Color Lerp(Color from, Color to, float progress)
	{
		int r, g, b, a;
		r = Lerp(from.R, to.R, progress);
		g = Lerp(from.G, to.G, progress);
		b = Lerp(from.B, to.B, progress);
		a = Lerp(from.A, to.A, progress);
		return Color.FromArgb(a, r, g, b);
	}
}
