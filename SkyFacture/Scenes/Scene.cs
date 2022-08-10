using System;

namespace SkyFacture.Scenes;
public abstract class Scene : IDisposable
{
	public abstract void Initialize();
	public abstract void Update(double delta);
	public abstract void Render(double delta);
	public abstract void Dispose();
}
