using System.IO;

namespace SkyFacture.Unical;

public abstract class ResourceLoader
{
	public abstract Stream GetFileStream(string fileName);
}
