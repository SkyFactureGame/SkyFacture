

using System;

namespace SkyFacture.Drawing;

public abstract class GLObj
{
	public readonly int handle;
	public GLObj(int handle)
	{
		this.handle = handle;
	}
}