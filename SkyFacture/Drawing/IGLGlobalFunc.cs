// The NiTiS-Dev licenses this file to you under the MIT license.

namespace SkyFacture.Drawing;
public interface IGLGlobalFunc : IGLFunc
{
	public static abstract void Enable();
	public static abstract void Disable();
}

public interface IGLFunc
{
	public void Use();
}