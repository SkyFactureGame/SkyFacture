namespace SkyFacture.IO;

public abstract class FileManager
{
	public abstract bool FileExists(string path, StorageType type);
	public abstract FileToken GetFile(string path, StorageType type);
}
