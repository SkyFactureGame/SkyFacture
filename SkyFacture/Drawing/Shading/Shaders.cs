

namespace SkyFacture.Drawing.Shading;
public class Shaders
{
	public static readonly DefaultShader DefShader;
	public static Shader Current { get; set; }
	static Shaders()
	{
		DefShader = new();
		Current = DefShader;
	}
}
