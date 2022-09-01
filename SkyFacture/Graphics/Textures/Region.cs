namespace SkyFacture.Graphics.Textures;
public class Region
{
	private readonly Sprite sprite;
	public readonly vec2 Begin, End;
	public Region(Sprite sprite, vec2 begin, vec2 end)
	{
		this.sprite = sprite;
		Begin = begin;
		End = end;
	}

	public static explicit operator Sprite(Region region)
		=> region.sprite;
}
