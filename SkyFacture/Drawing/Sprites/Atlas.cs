using System;
using System.IO;
using System.Reflection;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SkyFacture.Drawing.Sprites;
public static class Atlas
{
	public static readonly Region2D White, Black, Debug, Transperent;
	private static readonly Dictionary<string, Texture2D> Textures = new(16);
	private static readonly Dictionary<string, Region2D> Regions = new(128);
	public static Region2D? Region(string name)
		=> Regions.GetValueOrDefault(name);
	public static void LoadInternalRegions()
	{
		Assembly asm = typeof(Atlas).Assembly;
		string[] resourceFiles = asm.GetManifestResourceNames();

		Dictionary<string, AliasFile> aliases = new(4);

		List<(string OriginalName, string FileName, string Extension)> sortedFileNames = new();
		#region Sorting files
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

			string extension = Path.GetExtension(fileName).Substring(1);
			switch (extension)
			{
				case "alias":
					sortedFileNames.Insert(0, (resource, Path.GetFileNameWithoutExtension(fileName), "alias"));

					break;
				default:
					sortedFileNames.Add((resource, Path.GetFileNameWithoutExtension(fileName), extension));
					break;
			}
		}
		sortedFileNames = sortedFileNames.OrderByDescending((a) =>
		{
			return a.Extension switch
			{
				"alias" => 10,
				_ => 1,
			};
		}).ToList();
		#endregion

		foreach ((string OriginalName, string FileName, string Extension) file in sortedFileNames)
		{
			using Stream stream = asm.GetManifestResourceStream(file.OriginalName)!;

			if (file.Extension == "alias")
			{
				using StreamReader reader = new StreamReader(stream);
				IDeserializer deserializer = new DeserializerBuilder()
					.WithNamingConvention(UnderscoredNamingConvention.Instance)
					.Build();
				AliasFile alias = deserializer.Deserialize<AliasFile>(reader.ReadToEnd());
				aliases.Add(file.FileName, alias);
			}
			else if (file.Extension == "png")
			{
				aliases.TryGetValue(file.FileName, out AliasFile? alias);
				alias ??= new();
				Texture2D texture = new(stream, alias.Blending);
				foreach (TextureRegion reg in alias.Textures)
				{
					vec2i pos = ParseVec2(reg.Position), size = ParseVec2(reg.Size);

					Region2D region = new(texture, FromPosToPoint(pos, texture.Width, texture.Height), FromPosToPoint(pos + size, texture.Width, texture.Height));
					Regions.Add(reg.Name ?? throw new Exception("Where name?"), region);
				}
			}
		}
	}
	[System.Diagnostics.DebuggerStepThrough]
	private static vec2i ParseVec2(string? str)
	{
		if (str is null) return default;

		int splitIndex = str.IndexOf(',');
		string left = str.Substring(0, splitIndex);
		string right = str.Substring(splitIndex + 1);

		return new(Int32.Parse(left), Int32.Parse(right));
	}
	private static vec2 FromPosToPoint(vec2i pos, int width, int height)
		=> new(pos.X / (float)width, pos.Y / (float)height);
}
