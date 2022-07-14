// The NiTiS-Dev licenses this file to you under the MIT license.

using System;

namespace SkyFacture.Drawing;

public interface IGLObj : IDisposable
{
	public int Handle { get; }
	public void Bind();
}