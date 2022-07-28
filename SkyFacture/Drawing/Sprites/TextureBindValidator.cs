using OpenTK.Graphics.OpenGL;

namespace SkyFacture.Drawing;

public static class TextureBindValidator
{
	private static readonly Dictionary<i32, i32> targets = new(8);
	private static TextureUnit activeUnit = TextureUnit.Texture0;
	public static void ActivateUnit(TextureUnit unit = TextureUnit.Texture0)
	{
		if (activeUnit != unit)
		{
			activeUnit = unit;
			GL.ActiveTexture(unit);
		}
	}
}