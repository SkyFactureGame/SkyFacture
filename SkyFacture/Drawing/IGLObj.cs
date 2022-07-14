

using System;

namespace SkyFacture.Drawing;

public interface IGLObj : IDisposable
{
	public int Handle { get; }
	public void Bind();
}