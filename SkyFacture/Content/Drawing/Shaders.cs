using SkyFacture.Graphics.Shaders;

namespace SkyFacture.Content.Drawing;
public static class Shaders
{
	public static readonly ShaderProgram Default;
	static Shaders()
	{
		Default = ShaderProgram.Create(FM, "default", "default");
	}
}
