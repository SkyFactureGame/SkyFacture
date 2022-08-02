using SkyFacture.Extensions;
using SkyFacture.IO;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using static SkyFacture.IO.StorageType;

namespace SkyFacture.Desktop.IO;
public class DesktopFileManager : FileManager
{
	private readonly string[] internalFiles;
	private readonly Assembly asm;
	public DesktopFileManager(Assembly internalResourceAssembly)
	{
		asm = internalResourceAssembly;
		internalFiles = internalResourceAssembly.GetManifestResourceNames();
	}
	private static string ResolvePath(string path)
		=> path.Replace('/', '.').Replace('\\', '.');
	public override bool FileExists(string path, StorageType type)
	{
		return type switch
		{
			GameSource => InternalFileExists(ResolvePath(path)),
			Machine => File.Exists(path),
			_ => false
		};
	}
	private bool InternalFileExists(string path)
		=> internalFiles.Contains(path);
	public override FileToken GetFile(string path, StorageType type)
	{
		return type switch
		{
			GameSource => new InternalFileToken(asm, path),
			Machine => new LocalFileToken(path),
			_ => throw new ArgumentException("Invalid StorageType", nameof(type)),
		};
	}
}
