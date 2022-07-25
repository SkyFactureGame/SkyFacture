using System;

namespace SkyFacture;
public static class MathA
{
	public static float Range(float value, float maxValue)
		=> maxValue == value ? maxValue : value % maxValue;
	public static double Range(double value, double maxValue)
		=> maxValue == value ? maxValue : value % maxValue;
	public static byte Range(byte value, byte maxValue)
		=> unchecked((byte)(maxValue == value ? maxValue : value % maxValue));
	public static int Range(int value, int maxValue)
		=> unchecked(maxValue == value ? maxValue : value % maxValue);
	public static int Lerp(int left, int right, float value)
	{
		int min = Math.Min(left, right);
		int max = Math.Max(left, right);
		int delta = max - min;
		int diff = (int)(delta * value);
		return min + diff;
	}
}
