using SkyFacture.IO;
using System.IO;
using System.Reflection;

namespace SkyFacture.Desktop.IO;
public class InternalFileToken : FileToken
{
	private readonly Assembly asm;
	public InternalFileToken(Assembly asm, string path) : base(path)
	{
		this.asm = asm;
	}
	public override Stream OpenForRead()
		=> asm.GetManifestResourceStream(path)!;
	public override Stream OpenForWrite()
		=> asm.GetManifestResourceStream(path)!;
}
