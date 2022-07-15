// The NiTiS-Dev licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkyFacture.IO;

public class AssemblyResourceLoader : Unical.ResourceLoader
{
	private readonly Assembly asm;
	private readonly Dictionary<string, string> pathMapping;
	public AssemblyResourceLoader(Type type) : this(type.Assembly) { }
	public AssemblyResourceLoader(Assembly asm)
	{
		this.asm = asm;
		this.pathMapping = new Dictionary<string, string>();
	}
	public override string[]? GetFileNames()
	{
		return null;
	}
	public override Stream? GetFileStream(string fileName)
	{
		string truePath = pathMapping.ContainsKey(fileName) ? pathMapping[fileName] : fileName;

		return asm.GetManifestResourceStream(truePath);
	}
}
