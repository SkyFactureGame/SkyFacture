using System.IO;

namespace SkyFacture.IO;

public abstract class FileManager
{
	public abstract bool FileExists(string path, StorageType type);
	public abstract FileToken GetFile(string path, StorageType type);
	public FileToken Internal(string path)
		=> GetFile(path, StorageType.GameSource);
	public FileToken External(string path)
		=> GetFile(path, StorageType.Machine);
	public string InternalRead(string path)
	{
		using Stream stream = Internal(path).OpenForRead();
		using StreamReader sr = new(stream);
		return sr.ReadToEnd();
	}
	public string ExternalRead(string path)
	{
		using Stream stream = External(path).OpenForRead();
		using StreamReader sr = new(stream);
		return sr.ReadToEnd();
	}
}
