using System.Collections.Generic;
using System.Linq;

namespace SkyFacture.Content;

public static class Registry<T> where T : notnull
{
	private static readonly Dictionary<Id, T> values;
	public static bool Reg(Id id, T value)
	{
		if (values.ContainsKey(id))
		{
			return false;
		}
		else
		{
			values.Add(id, value);
			return true;
		}
	}
	public static IEnumerable<T> GetAll()
		=> values.Values;
	static Registry()
	{
		values = new(32);
	}
}