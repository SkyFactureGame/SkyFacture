using System.IO;

namespace SkyFacture.Graphics.Textures;

public abstract class SpriteLoader
{
	public abstract void InitializeData(Sprite sprite, Stream imageStream);
}
