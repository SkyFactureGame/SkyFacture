using System.Runtime.InteropServices;

namespace SkyFacture.Content;

[StructLayout(LayoutKind.Sequential)]
public readonly struct GameVersion
{
	/// <summary>
	/// Version (Major)
	/// </summary>
	public readonly ushort V;
	/// <summary>
	/// Revision
	/// </summary>
	public readonly ushort R;

	public GameVersion(ushort v, ushort r)
	{
		this.V = v;
		this.R = r;
	}
	public override string ToString()
		=> $"V{V}r{R}";
}
