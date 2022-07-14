// The NiTiS-Dev licenses this file to you under the MIT license.

using SkyFacture.Content.Abstractions;

namespace SkyFacture.Content.Registry;
public static class Registrator
{
	internal static void Paste<T>(uint id, T item) where T : IContentType
	{
		LocalReestr<T>.Paste(id, item);
	}
	internal static void ReserveIDs<T>(uint limit) where T : IContentType
	{
		LocalReestr<T>.CheckCapacity(limit);
		LocalReestr<T>.newRegistryID = limit;
	}
	public static uint Registry<T>(T item) where T : IContentType
	{
		return LocalReestr<T>.Registry(item);
	}
	public static T? Get<T>(uint id) where T : IContentType
	{
		if (id > LocalReestr<T>.Capacity.FromCapacityToIndex())
		{
			return default;
		}

		return LocalReestr<T>.Get(id);
	}
	private static class LocalReestr<T> where T : IContentType
	{
		private static T[] items;
		public static uint newRegistryID;
		public static uint Capacity { get; private set; }
		public static T? Get(uint id)
		{
			return items[id];
		}
		public static uint Registry(T item)
		{
			CheckCapacity();
			items[newRegistryID++] = item;
			return newRegistryID - 1;
		}
		public static void Paste(uint id, T item)
		{
			items[id] = item;
		}
		public static void CheckCapacity(uint? requiredID = null)
		{
			requiredID ??= newRegistryID;

			if (requiredID >= Capacity)
			{
				uint newCapacity = Capacity * 2;
				while (requiredID > newCapacity)
				{
					newCapacity *= 2;
				}

				Capacity = newCapacity;
				T[] newItems = new T[Capacity];
				
				items.CopyTo(newItems, 0);
				items = newItems;
			}
		}
		static LocalReestr()
		{
			Capacity = 1;
			items = new T[Capacity];
		}
	}
}
