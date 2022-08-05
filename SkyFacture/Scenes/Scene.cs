using System;

namespace SkyFacture.Scenes;
public class Scene : IDisposable
{
	public virtual void Initialize()
	{
		Console.WriteLine("Init");
	}
	public virtual void Update()
	{
	}
	public virtual void Render()
	{

	}
	public virtual void Dispose()
	{

	}
}
