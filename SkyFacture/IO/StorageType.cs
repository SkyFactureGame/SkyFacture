namespace SkyFacture.IO;

public enum StorageType : byte
{
	/// <summary>
	/// Local machine file (like mods)
	/// </summary>
	Machine = 0,
	/// <summary>
	/// Internal file
	/// </summary>
	GameSource = 1,
}
