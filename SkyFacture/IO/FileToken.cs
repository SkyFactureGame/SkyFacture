using System.IO;

namespace SkyFacture.IO;

public abstract class FileToken
{
	protected readonly string path;
	public FileToken(string path) => this.path = path;
	public abstract Stream OpenForRead();
	public abstract Stream OpenForWrite();
	public string ReadAllStrings()
	{
		using Stream stream = OpenForRead();
		using StreamReader sr = new(stream);
		return sr.ReadToEnd();
	}
	public byte[] ReadAll()
	{
		using Stream stream = OpenForRead();
		byte[] alloc = new byte[stream.Length];
		stream.Read(alloc, 0, (int)stream.Length);
		return alloc;
	}
}
