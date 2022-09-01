namespace SkyFacture;

public static class EngineEvents
{
	public static readonly Event
		Update,
		Render,
		Exit;

	static EngineEvents()
	{
		Update = new(1);
		Render = new(2);
		Exit = new(-1);
	}
}