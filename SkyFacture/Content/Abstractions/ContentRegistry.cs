// The NiTiS-Dev licenses this file to you under the MIT license.

using SkyFacture.Content.Registry;

namespace SkyFacture.Content.Abstractions;
public abstract class ContentRegistry<TSelf, T> 
	where T : IContentType
	where TSelf : ContentRegistry<TSelf, T>
{
	public abstract void Registry();
	protected uint Reg(T item)
	{
		return Registrator.Registry(item);
	}
}
