// The NiTiS-Dev licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFacture;
public readonly struct GameVersion : IEquatable<GameVersion>, IComparable<GameVersion>
{
	public readonly byte version;
	public readonly ushort revision;
	public GameVersion(byte V, ushort r)
	{
		this.version = V;
		this.revision = r;
	}
	public override string ToString()
		=> $"V{version}r{revision}";

	public override bool Equals(object? obj)
		=> obj is GameVersion gv && Equals(gv);
	public bool Equals(GameVersion other) 
		=> this.version == other.version && this.revision == other.revision;
	public int CompareTo(GameVersion other)
		=> this.CalculateValue() - other.CalculateValue();
	public int CalculateValue()
		=> (this.version << sizeof(ushort) * 8) + revision;

	public override int GetHashCode()
		=> CalculateValue();


	public static bool operator ==(GameVersion left, GameVersion right)
		=> left.Equals(right);
	public static bool operator !=(GameVersion left, GameVersion right)
		=> !left.Equals(right);
	public static bool operator <(GameVersion left, GameVersion right)
		=> left.CompareTo(right) < 0;
	public static bool operator <=(GameVersion left, GameVersion right)
		=> left.CompareTo(right) <= 0;
	public static bool operator >(GameVersion left, GameVersion right)
		=> left.CompareTo(right) > 0;
	public static bool operator >=(GameVersion left, GameVersion right)
		=> left.CompareTo(right) >= 0;
}
