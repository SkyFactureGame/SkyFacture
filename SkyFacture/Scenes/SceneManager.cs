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
	public void Render()
	{
		scene?.Render();
	}
	public void Update()
	{
		scene?.Update();
	}
	public void Dispose()
	{
		this.scene?.Dispose();
	}
}
