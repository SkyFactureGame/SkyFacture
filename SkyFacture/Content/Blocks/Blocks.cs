using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFacture.Content.Blocks;
public static class Blocks
{
	public static readonly Block
		//Air
		Air,
		
		//Env
		Dirt, Grass;
	static Blocks()
	{
		Air = new("air");
		Dirt = new("dirt");
		Grass = new("grass");
	}
}
