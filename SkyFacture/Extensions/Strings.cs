namespace SkyFacture.Extensions;
public static class Strings
{
	public static string ReplaceOnce(this string str, int index, char replace)
		=> str.ReplaceOnce(index, 1, replace);
	public static string ReplaceOnce(this string str, int index, int size, char replace)
	{
		if (index < 0 || index > str.Length - 1)
			throw new System.ArgumentException(null, nameof(index));
		return str.Remove(index, size).Insert(index, replace.ToString());
	}
	public static string ReplaceOnce(this string str, char target, char replace)
	{
		int index = str.IndexOf(target);
		if (index < 0 || index > str.Length - 1)
			throw new System.ArgumentException(null, nameof(target));
		return str.Remove(index, 1).Insert(index, replace.ToString());
	}
	public static string ReplaceOnce(this string str, int index, int size, string replace)
	{
		if (index < 0 || index > str.Length - 1)
			throw new System.ArgumentException(null, nameof(index));
		return str.Remove(index, size).Insert(index, replace.ToString());
	}
	public static string ReplaceOnce(this string str, string target, string replace)
	{
		int index = str.IndexOf(target);
		if (index < 0 || index > str.Length - 1)
			throw new System.ArgumentException(null, nameof(target));
		return str.Remove(index, replace.Length).Insert(index, replace);
	}
}
