using System.IO;
using System.IO.Compression;

namespace SkyFacture.IO;

/// <remarks>
/// Stolen from <seealso href="https://github.com/StayOnSofa/Perge/blob/6c7d9154ff2082f754a8c6ab2f7c3e84ab08f162/Perge/Assets/_Project/Core/Generals/Packing/Deflate.cs#L6"> StayOnSofa/Perge </seealso>
/// </remarks>
public static class DeflateCompression
{
	public static byte[] Compress(byte[] data)
	{
		using MemoryStream memoryStream = new();
		using DeflateStream deflateStream = new(memoryStream, CompressionMode.Compress);

		deflateStream.Write(data, 0, data.Length);
		return memoryStream.ToArray();
	}
	public static void Compress(Stream stream, byte[] data)
	{
		using DeflateStream deflateStream = new(stream, CompressionMode.Compress);

		deflateStream.Write(data, 0, data.Length);
	}
	public static byte[] Decompress(byte[] data)
	{
		using MemoryStream decompressedStream = new();
		using MemoryStream compressStream = new(data);
		using DeflateStream deflateStream = new(compressStream, CompressionMode.Decompress);

		deflateStream.CopyTo(decompressedStream);
		return decompressedStream.ToArray();
	}
	public static void Decompress(Stream stream, byte[] data)
	{
		using MemoryStream compressStream = new(data);
		using DeflateStream deflateStream = new(compressStream, CompressionMode.Decompress);

		deflateStream.CopyTo(stream);
	}
	public static void Decompress(Stream stream, Stream data)
	{
		using DeflateStream deflateStream = new(data, CompressionMode.Decompress);

		deflateStream.CopyTo(stream);
	}
}
