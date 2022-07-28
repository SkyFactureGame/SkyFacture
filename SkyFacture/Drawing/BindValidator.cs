using System;

namespace SkyFacture.Drawing;

public static class BindValidator
{
	private static readonly Dictionary<BindTarget, Int32> targets = new(8);
	public static void Bind(BindTarget target, int handle)
		=> targets[target] = handle;
	public static bool Binded(BindTarget target, int handle)
		=> targets[target] == handle;
	public static bool ShoudBind(BindTarget target, int handle)
	{
		bool a = targets[target] == handle;
		if (!a) targets[target] = handle;
		return !a;
	}
	static BindValidator()
	{
		foreach (BindTarget posibleTarget in Enum.GetValues<BindTarget>())
		{
			targets[posibleTarget] = 0;
		}
	}
}

public enum BindTarget : byte
{
	Texture = 1,
	VertexArray = 2,
	Buffer = 3,
	Shader = 4
}