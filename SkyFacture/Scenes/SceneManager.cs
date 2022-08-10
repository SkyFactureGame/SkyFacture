using System;

namespace SkyFacture.Scenes;

public class SceneManager : IDisposable
{
	private Scene? scene;
	public void ChangeScene(Scene scene)
	{
		this.scene?.Dispose();
		this.scene = scene;
		this.scene.Initialize();
	}
	public void Render(double delta)
	{
		scene?.Render(delta);
	}
	public void Update(double delta)
	{
		scene?.Update(delta);
	}
	public void Dispose()
	{
		this.scene?.Dispose();
	}
}
