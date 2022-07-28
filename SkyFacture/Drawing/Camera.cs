

namespace SkyFacture.Drawing;
public class Camera
{
	public vec3 Position { get; set; }
	public float Scale { get; set; } = 1f;
	public Camera() : this(default) { }
	public Camera(vec3 position)
	{
		Position = position;
	}
	public mat4 GetProjection()
		=> GetProjection(Screen.Width, Screen.Height);
	public mat4 GetTranslation()
		=> mat4.CreateTranslation(Position);
	public mat4 GetProjection(float sizeX, float sizeY)
		=> mat4.CreateOrthographic(sizeX, sizeY, -1000f, 10000f);
}
