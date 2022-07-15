// The NiTiS-Dev licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFacture.Text;

public partial class Language
{
	public readonly string Code;
	private ushort regID;
	private readonly IDictionary<string, string> translates;
	public Language(string langCode, IDictionary<string, string>? values = null)
	{
		this.Code = langCode;
		translates ??= values ?? new Dictionary<string, string>(1);
	}
	public string this[string key]
	{
		get => translates.ContainsKey(key) ? translates[key] : key;
		set => translates[key] = value;
	}
	public void Select()
	{
		if (regID != 0)
			usingID = regID;
		else
			throw new Exception("Language exception: not registred");
	}
}
