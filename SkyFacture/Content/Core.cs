using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SkyFacture.IO;

namespace SkyFacture.Content;

public static class Core
{
	public static IView View { get; internal set; }
	public static GL Gl { get; internal set; }
	public static FileManager FM { get; internal set; }
}
