using System.IO;

namespace SkyFacture.Unical;

public abstract class ResourceLoader
{
	public abstract Stream GetFileStream(string fileName);
	/// <summary>
	/// Returns the names of all files
	/// </summary>
	/// <returns>
	/// <see langword="null"/> when feature is not supported
	/// </returns>
	public abstract string[]? GetFileNames();
}
