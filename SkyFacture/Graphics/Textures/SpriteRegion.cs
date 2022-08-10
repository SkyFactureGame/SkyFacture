namespace SkyFacture.Graphics.Textures;
public class SpriteRegion
{
	private readonly Sprite sprite;
	public readonly vec2 Begin, End;
	public SpriteRegion(Sprite sprite, vec2 begin, vec2 end)
	{
		this.sprite = sprite;
		Begin = begin;
		End = end;
	}

	public static explicit operator Sprite(SpriteRegion region)
		=> region.sprite;
}
