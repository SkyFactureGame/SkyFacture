using System;

namespace SkyFacture.Scenes;
public abstract class Scene : IDisposable
{
	public abstract void Initialize();
	public abstract void Update();
	public abstract void Render();
	public abstract void Dispose();
}
