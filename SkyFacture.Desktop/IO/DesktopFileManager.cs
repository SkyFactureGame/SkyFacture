using SkyFacture.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static SkyFacture.IO.StorageType;

namespace SkyFacture.Desktop.IO;
public class DesktopFileManager : FileManager
{
	private readonly Dictionary<string, string> redirects = new(128);
	private readonly Assembly asm;
	public DesktopFileManager(Assembly internalResourceAssembly)
	{
		asm = internalResourceAssembly;
		
		foreach (string path in internalResourceAssembly.GetManifestResourceNames())
		{
			string fileName = path;
			while (true)
			{
				int lastIndex, firstIndex;

				firstIndex = fileName.IndexOf('.');
				lastIndex = fileName.LastIndexOf('.');

				if (lastIndex == firstIndex)
					break;

				fileName = fileName.Substring(firstIndex + 1, fileName.Length - firstIndex - 1);
			}

			redirects.Add(fileName, path);
		}
	}
	public override bool FileExists(string path, StorageType type)
	{
		return type switch
		{
			GameSource => redirects.ContainsKey(path) || redirects.ContainsValue(path),
			Machine => File.Exists(path),
			_ => false
		};
	}
	public override FileToken GetFile(string path, StorageType type)
	{
		return type switch
		{
			GameSource => new InternalFileToken(asm, redirects.ContainsKey(path) ? redirects[path] : path),
			Machine => new LocalFileToken(path),
			_ => throw new ArgumentException("Invalid StorageType", nameof(type)),
		};
	}
}
