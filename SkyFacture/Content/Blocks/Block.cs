using SkyFacture.Content.Types;
using SkyFacture.Graphics.Textures;
using System.IO;

namespace SkyFacture.Content.Blocks;

public class Block : GameContent
{
	public const string TypeName = "block";
	public Block(string name) : base(TypeName, name)
	{
	}
}
