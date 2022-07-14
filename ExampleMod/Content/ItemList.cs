

using ExampleMod.Content.Items;
using SkyFacture.Content.Abstractions;

namespace ExampleMod.Content;
public class ItemList : ContentRegistry<ItemList, Item>
{
	public override void Registry()
	{
		Reg(new ExampleItem(50));

	}
}
