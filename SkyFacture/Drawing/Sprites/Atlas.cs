// The NiTiS-Dev licenses this file to you under the MIT license.

using System.IO;
using System.Reflection;

namespace SkyFacture.Drawing.Sprites;
public static class Atlas
{
	public static readonly Region2D White, Black, Debug, Transperent;
	private static readonly Dictionary<string, Texture2D> Textures = new(16);
	private static readonly Dictionary<string, Region2D> Regions = new(128);

	static Atlas()
	{
		//FIXME: Temp solve
		Assembly asm = typeof(Atlas).Assembly;
		string[] resourceFiles = asm.GetManifestResourceNames();

		Dictionary<string, AliasFile> aliases = new(4);

		foreach (string resource in resourceFiles)
		{
			string fileName = resource;
			int points = 0;
			for (int i = 0; i < resource.Length; i++)
			{
				if (resource[i] == '.') points++;
			}
			for (int i = 1; i < points; i++)
			{
				int pointIndex = fileName.IndexOf('.');
				fileName = fileName.Substring(0, pointIndex) + "/" + fileName.Substring(pointIndex + 1);
			}
			System.Console.WriteLine(fileName);
			using Stream stream = asm.GetManifestResourceStream(resource)!;
		}
	}
}
