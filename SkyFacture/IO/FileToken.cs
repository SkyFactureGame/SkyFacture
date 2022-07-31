using System.IO;

namespace SkyFacture.IO;

public abstract class FileToken
{
	protected readonly string path;
	public FileToken(string path) => this.path = path;
	public abstract Stream OpenForRead();
	public abstract Stream OpenForWrite(); 
}
