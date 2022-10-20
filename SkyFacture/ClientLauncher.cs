using SkyFacture.Content;
using SkyFacture.Extensions;
using SkyFacture.Graphics.Textures;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SkyFacture;

public abstract class ClientLauncher
{
	public virtual void Init()
	{
		LoadInternalResources();
	}

	private static void LoadInternalResources()
	{
		Assembly asm = typeof(ClientLauncher).Assembly;

		string[] names = asm.GetManifestResourceNames()
			.OrderByDescending(s => Path.GetExtension(s) is ".atlas")
			.ToArray();

		foreach (string name in names)
		{
			string normalizedFileName = name.NormalizeManifestName();
			string fileName = Path.GetFileNameWithoutExtension(normalizedFileName);
			string ext = Path.GetExtension(normalizedFileName);

			if (ext is ".atlas")
			{
			}
			else if (ext is ".png")
			{
				using Stream stream = asm.GetManifestResourceStream(name)!;
				Sprite sprite = new(stream);
				Reg(fileName, sprite);
			}
		}
	}

	private static void Reg<T>(string name, T value) where T : notnull
		=> Registry<T>.Reg(Id.Of(name), value);
}
