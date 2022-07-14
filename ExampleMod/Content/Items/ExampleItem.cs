// The NiTiS-Dev licenses this file to you under the MIT license.

using SkyFacture.Content.Items;
using SkyFacture.Content.Players;
using SkyFacture.Execution;

namespace ExampleMod.Content.Items;
public class ExampleItem : Item
{
	public ExampleItem(ushort maxStack) : base(maxStack)
	{
	}
	public override bool CanUse(Observer obsy, Player player, Stack stack)
	{
		stack.Substract(1);
		return true;
	}
}
