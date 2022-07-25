﻿

namespace SkyFacture.Drawing;
public class Camera
{
	public static Camera? Current { get; set; }
	public vec3 Position { get; set; } = default;
	public void Select()
		=> Current = this;
	public mat4 GetView()
		=> GetView(Screen.Width, Screen.Height);
	public mat4 GetTranslation()
		=> mat4.CreateTranslation(Position);
	public mat4 GetView(float sizeX, float sizeY)
		=> mat4.CreateOrthographic(sizeX, sizeY, -1000f, 10000f);
}
