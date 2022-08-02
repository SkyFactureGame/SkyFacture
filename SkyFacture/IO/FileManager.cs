namespace SkyFacture.IO;

public abstract class FileManager
{
	public abstract bool FileExists(string path, StorageType type);
	public abstract FileToken GetFile(string path, StorageType type);
	public FileToken Internal(string path)
		=> GetFile(path, StorageType.GameSource);
	public FileToken External(string path)
		=> GetFile(path, StorageType.Machine);
}
