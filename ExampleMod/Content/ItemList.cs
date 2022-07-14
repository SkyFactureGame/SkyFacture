// The NiTiS-Dev licenses this file to you under the MIT license.

using ExampleMod.Content.Items;
using SkyFacture.Content.Abstractions;
using SkyFacture.Content.Items;

namespace ExampleMod.Content;
public class ItemList : ContentRegistry<ItemList, Item>
{
	public override void Registry()
	{
		Reg(new ExampleItem(50));

	}
}
