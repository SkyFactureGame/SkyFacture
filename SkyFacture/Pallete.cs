using System;
using System.Drawing;

namespace SkyFacture;
public static class Palette
{
	public static readonly Color
		Red, Yellow, Blue, Green, Turquoise, Pink, White, Black, Transperent;
	public static Color DiscoHalf
		=> Rainbow(DateTime.Now.Millisecond / 0_500f);
	public static Color Disco
		=> Rainbow(DateTime.Now.Millisecond / 1_000f);
	public static Color Disco2
		=> Rainbow(DateTime.Now.Millisecond / 2_000f + (DateTime.Now.Second % 2 * 0.5f));
	public static Color Disco5
		=> Rainbow(DateTime.Now.Millisecond / 5_000f + (DateTime.Now.Second % 5 * 0.2f));
	public static Color Disco10
		=> Rainbow(DateTime.Now.Millisecond / 10_000f + (DateTime.Now.Second % 10 * 0.1f));
	public static Color Rainbow(float progress)
	{
		float div = (Math.Abs(progress % 1) * 6);
		int ascending = (int)((div % 1) * 255);
		int descending = 255 - ascending;

		switch ((int)div)
		{
			case 0:
				return Color.FromArgb(255, 255, ascending, 0);
			case 1:
				return Color.FromArgb(255, descending, 255, 0);
			case 2:
				return Color.FromArgb(255, 0, 255, ascending);
			case 3:
				return Color.FromArgb(255, 0, descending, 255);
			case 4:
				return Color.FromArgb(255, ascending, 0, 255);
			default: // case 5:
				return Color.FromArgb(255, 255, 0, descending);
		}
	}
	public static Color Lerp(Color left, Color right, float progress)
	{
		int r, g, b, a;
		r = SkyMath.Lerp(left.R, right.R, progress);
		g = SkyMath.Lerp(left.G, right.G, progress);
		b = SkyMath.Lerp(left.B, right.B, progress);
		a = SkyMath.Lerp(left.A, right.A, progress);
		return Color.FromArgb(a, r, g, b);
	}
	static Palette()
	{
		Red = Color.FromArgb(255, 0, 0);
		Yellow = Color.FromArgb(255, 255, 0);
		Green = Color.FromArgb(0, 255, 0);
		Turquoise = Color.FromArgb(0, 255, 255);
		Blue = Color.FromArgb(0, 0, 255);
		Blue = Color.FromArgb(255, 0, 255);

		Black = Color.FromArgb(0, 0, 0);
		White = Color.FromArgb(255, 255, 255);
		Transperent = Color.FromArgb(0, 0, 0, 0);
	}
}
public static class ColorExtensions
{
	public static Color Reverse(this Color color)
	{
		return Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
	}
}