using System;

namespace SkyFacture.Collections;
public static class ArrayFactory
{
	public static T[] Default<T>(uint size)
		=> new T[size];
	public static T[,] Default<T>(uint sizeX, uint sizeY)
		=> new T[sizeX, sizeY];
	public static T[,,] Default<T>(uint sizeX, uint sizeY, uint sizeZ)
		=> new T[sizeX, sizeY, sizeZ];
	public static T[] Initialized<T>(uint size) where T : struct
	{
		T[] values = new T[size];
		values.Initialize();
		return values;
	}
	public static T[,] Initialized<T>(uint sizeX, uint sizeY) where T : struct
	{
		T[,] values = new T[sizeX, sizeY];
		values.Initialize();
		return values;
	}
	public static T[,,] Initialized<T>(uint sizeX, uint sizeY, uint sizeZ) where T : struct
	{
		T[,,] values = new T[sizeX, sizeY, sizeZ];
		values.Initialize();
		return values;
	}
	public static T[] WithValue<T>(uint size, T value)
	{
		T[] values = new T[size];

		for (uint i = 0; i < size; i++)
		{
			values[i] = value;
		}
		return values;
	}
	public static T[,] WithValue<T>(uint sizeX, uint sizeY, T value)
	{
		T[,] values = new T[sizeX, sizeY];

		for (uint x = 0; x < sizeX; x++)
		{
			for (uint y = 0; y < sizeY; y++)
			{
				values[x, y] = value;
			}
		}
		return values;
	}
	public static T[,,] WithValue<T>(uint sizeX, uint sizeY, uint sizeZ, T value)
	{
		T[,,] values = new T[sizeX, sizeY, sizeZ];

		for (uint x = 0; x < sizeX; x++)
		{
			for (uint y = 0; y < sizeY; y++)
			{
				for (uint z = 0; z < sizeZ; z++)
				{
					values[x, y, z] = value;
				}
			}
		}
		return values;
	}
	public static T[] WithValue<T>(uint size, Func<uint, T> valueGet)
	{
		T[] values = new T[size];

		for (uint i = 0; i < size; i++)
		{
			values[i] = valueGet(i);
		}
		return values;
	}
	public static T[,] WithValue<T>(uint sizeX, uint sizeY, Func<uint, uint, T> valueGet)
	{
		T[,] values = new T[sizeX, sizeY];

		for (uint x = 0; x < sizeX; x++)
		{
			for (uint y = 0; y < sizeY; y++)
			{
				values[x, y] = valueGet(x, y);
			}
		}
		return values;
	}
	public static T[,,] WithValue<T>(uint sizeX, uint sizeY, uint sizeZ, Func<uint, uint, uint, T> valueGet)
	{
		T[,,] values = new T[sizeX, sizeY, sizeZ];

		for (uint x = 0; x < sizeX; x++)
		{
			for (uint y = 0; y < sizeY; y++)
			{
				for (uint z = 0; z < sizeZ; z++)
				{
					values[x, y, z] = valueGet(x, y, z);
				}
			}
		}
		return values;
	}
}
