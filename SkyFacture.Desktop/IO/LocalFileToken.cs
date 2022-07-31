using SkyFacture.IO;
using System.IO;

namespace SkyFacture.Desktop.IO;

public class LocalFileToken : FileToken
{
	public LocalFileToken(string path) : base(path) { }
	public override Stream OpenForRead()
		=> File.OpenRead(path);
	public override Stream OpenForWrite()
		=> File.OpenWrite(path);
}
