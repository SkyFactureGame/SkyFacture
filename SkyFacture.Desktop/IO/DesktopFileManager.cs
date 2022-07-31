using SkyFacture.IO;
using System;
using System.Linq;
using System.Reflection;

namespace SkyFacture.Desktop.IO;
public class DesktopFileManager : FileManager
{
	public DesktopFileManager(Assembly internalResourceAssembly)
	{
		string[] files = internalResourceAssembly.GetManifestResourceNames();
		for (int i = 0; i < files.Length; i++)
		{
			string path = files[i];
			int index = 0;
			int lastIndex = path.LastIndexOf('.'); // TODO
			do
			{
				index = path.IndexOf('.');
				path = path.Replace('.', '/');
			}
			while (index != lastIndex);
			Console.WriteLine(path);
		}
	}
	public override bool FileExists(string path, StorageType type)
	{
		return false;
	}
	public override FileToken GetFile(string path, StorageType type)
	{
		return new LocalFileToken(path);
	}
}
