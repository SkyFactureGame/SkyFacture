using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SkyFacture.Graphics.Textures;
using SkyFacture.IO;
using SkyFacture.Scenes;

namespace SkyFacture;

public static class Core
{
	public static IView View { get; internal set; }
	public static GL Gl { get; internal set; }
	public static FileManager FM { get; internal set; }
	public static SpriteLoader SL { get; internal set; }
	public static SceneManager SM { get; internal set; } 
}
