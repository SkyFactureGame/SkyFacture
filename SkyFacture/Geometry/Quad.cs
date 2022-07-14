// The NiTiS-Dev licenses this file to you under the MIT license.

using System;
using System.Runtime.InteropServices;

namespace SkyFacture.Geometry;

[StructLayout(LayoutKind.Sequential)]
public readonly struct Quad : IEquatable<Quad>
{
	private readonly float leftX, bottomY, rightX, topY;
	public readonly vec2 LeftBottom => new(leftX, bottomY); 
	public readonly vec2 LeftTop => new(leftX, topY); 
	public readonly vec2 RightBottom => new(rightX, bottomY); 
	public readonly vec2 RightTop => new(rightX, topY);
	public readonly vec2 Center => new((leftX + rightX) / 2, (bottomY + topY) / 2);
	public readonly float Width => rightX - leftX;
	public readonly float Hiegth => topY - bottomY;
	public Quad(float dx, float dy, float tx, float ty)
	{
		this.leftX = dx;
		this.bottomY = dy;
		this.rightX = tx;
		this.topY = ty;
	}
	public readonly override bool Equals(object? obj)
		=> obj is Quad quad && Equals(quad);
	public readonly bool Equals(Quad quad)
		=> this.topY == quad.topY
		&& this.rightX == quad.rightX
		&& this.bottomY == quad.bottomY
		&& this.leftX == quad.leftX;
	public readonly bool InBounds(Quad quad) 
		=>		InBounds(quad.LeftBottom)
			||	InBounds(quad.LeftTop)
			||	InBounds(quad.RightBottom)
			||	InBounds(quad.RightTop);
	public readonly bool InBounds(vec2 point)
	{
		if (point.X > rightX)
			return false;
		if (point.X < leftX)
			return false;
		if (point.Y > topY)
			return false;
		if (point.Y < bottomY)
			return false;

		return true;
	}
	public static bool operator |(Quad left, vec2 point)
		=> !left.InBounds(point);
	public static bool operator |(Quad left, Quad quad)
		=> !left.InBounds(quad);
	public static bool operator ==(Quad left, Quad right)
		=> left.Equals(right);
	public static bool operator !=(Quad left, Quad right)
		=> !(left == right);
	public override int GetHashCode()
		=> HashCode.Combine(leftX, rightX, bottomY, topY);
}
