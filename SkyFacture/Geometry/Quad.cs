

using System;
using System.Runtime.InteropServices;

namespace SkyFacture.Geometry;

[StructLayout(LayoutKind.Sequential)]
public readonly struct Quad : IEquatable<Quad>
{
	private readonly float leftX, bottomY, rightX, topY;
	public readonly vec2 LeftBottom => new(this.leftX, this.bottomY);
	public readonly vec2 LeftTop => new(this.leftX, this.topY);
	public readonly vec2 RightBottom => new(this.rightX, this.bottomY);
	public readonly vec2 RightTop => new(this.rightX, this.topY);
	public readonly vec2 Center => new((this.leftX + this.rightX) / 2, (this.bottomY + this.topY) / 2);
	public readonly float Width => this.rightX - this.leftX;
	public readonly float Heigth => this.topY - this.bottomY;
	public Quad(float dx, float dy, float tx, float ty)
	{
		this.leftX = dx;
		this.bottomY = dy;
		this.rightX = tx;
		this.topY = ty;
	}
	public override readonly bool Equals(object? obj)
		=> obj is Quad quad && Equals(quad);
	public readonly bool Equals(Quad quad)
		=> this.topY == quad.topY
		&& this.rightX == quad.rightX
		&& this.bottomY == quad.bottomY
		&& this.leftX == quad.leftX;
	public readonly bool InBounds(Quad quad)
		=> InBounds(quad.LeftBottom)
			|| InBounds(quad.LeftTop)
			|| InBounds(quad.RightBottom)
			|| InBounds(quad.RightTop);
	public readonly bool InBounds(vec2 point)
	{
		if (point.X > this.rightX)
			return false;
		if (point.X < this.leftX)
			return false;
		if (point.Y > this.topY)
			return false;
		if (point.Y < this.bottomY)
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
		=> HashCode.Combine(this.leftX, this.rightX, this.bottomY, this.topY);
}
