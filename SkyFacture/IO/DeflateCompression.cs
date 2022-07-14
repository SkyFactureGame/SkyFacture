// The NiTiS-Dev licenses this file to you under the MIT license.

using System;
using System.IO;
using System.IO.Compression;

namespace SkyFacture.IO;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Stolen from <seealso href="https://github.com/StayOnSofa/Perge/blob/6c7d9154ff2082f754a8c6ab2f7c3e84ab08f162/Perge/Assets/_Project/Core/Generals/Packing/Deflate.cs#L23"> StayOnSofa/Perge </seealso>
/// </remarks>
public class DeflateCompression
{
	public static byte[] Compress(byte[] data)
	{
		using MemoryStream memoryStream = new();
		using DeflateStream deflateStream = new(memoryStream, CompressionMode.Compress);

		deflateStream.Write(data, 0, data.Length);
		return memoryStream.ToArray();
	}
	public static byte[] Decompress(byte[] data)
	{
		using MemoryStream decompressedStream = new();
		using MemoryStream compressStream = new(data);
		using DeflateStream deflateStream = new(compressStream, CompressionMode.Decompress);

		deflateStream.CopyTo(decompressedStream);
		return decompressedStream.ToArray();
	}
}
