namespace SkyFacture;
public static class SkyMath
{
	public static int Lerp(int from, int to, float progress)
		=> (int)(from + (to - from) * progress);
	public static float Lerp(float from, float to, float progress)
		=> from + (to - from) * progress;
	public static double Lerp(double from, double to, double progress)
		=> from + (to - from) * progress;
}
