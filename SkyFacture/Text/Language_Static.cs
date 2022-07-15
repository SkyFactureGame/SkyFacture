// The NiTiS-Dev licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFacture.Text;
public partial class Language
{
	private static ushort lastID = 0, usingID = 0;
	private static readonly List<Language> languages = new(4);
	public static IEnumerable<Language> GetLanguages()
		=> languages;
	public static Language? GetLanguage(ushort id)
		=> id == 0 ? null : id > lastID ? null : languages[id - 1];
	public static ushort LanguageRegistry(Language lang)
	{
		if (languages.Where(l => l.Code == lang.Code).Count() > 0)
			return 0;
		
		lastID++;

		languages.Add(lang);
		lang.regID = lastID;

		return lastID;
	}
}
