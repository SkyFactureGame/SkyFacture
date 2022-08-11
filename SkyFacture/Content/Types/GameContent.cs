using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFacture.Content.Types;
public abstract class GameContent
{
	public readonly string ContentTypeName;
	public readonly string Name;
	public GameContent(string typeName, string idName)
	{
		ContentTypeName = typeName;
		Name = idName;
	}
}
