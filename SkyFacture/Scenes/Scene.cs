// The NiTiS-Dev licenses this file to you under the MIT license.
using System;

namespace SkyFacture.Scenes;
public abstract class Scene : IDisposable
{
	public abstract void Init();
	public abstract void Render();
	public abstract void Update();
	public abstract void Destroy();
	public void Dispose() => Destroy();
}