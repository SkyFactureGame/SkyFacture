// The NiTiS-Dev licenses this file to you under the MIT license.

using SkyFacture.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFacture.Drawing.Sprites;
public class Region2D
{
	private readonly Texture2D texture;
	private readonly vec2 beginEdge, endEdge;
	public Region2D(Texture2D texture, vec2 lowLeftPoint, vec2 rightTopPoint)
	{
		this.texture = texture;
		this.beginEdge = lowLeftPoint;
		this.endEdge = rightTopPoint;
	}


	public static explicit operator Quad(Region2D region)
		=> new(region.beginEdge.X, region.beginEdge.Y, region.endEdge.X, region.endEdge.Y);
}
