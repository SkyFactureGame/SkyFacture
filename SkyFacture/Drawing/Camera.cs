

using System;

namespace SkyFacture.Drawing;
public class Camera
{
	private mat4 projMatrix;
	public vec3 Position { get; set; }
	public float Scale { get; set; } = 1f;
	public Camera() : this(default) { }
	public Camera(vec3 position)
	{
		Position = position;
		projMatrix = mat4.CreateOrthographic(Screen.Width, Screen.Height, -1000f, 10000f);
	}
	public void UpdateProjMatrix(float width, float height) 
		=> projMatrix = mat4.CreateOrthographic(width, height, -1000f, 10000f);
	public mat4 GetOrthoProj()
		=> projMatrix;
	public mat4 GetTranslation()
		=> mat4.CreateTranslation(Position);
}
