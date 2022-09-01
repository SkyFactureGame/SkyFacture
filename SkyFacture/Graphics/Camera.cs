using Silk.NET.Maths;

namespace SkyFacture.Graphics;

public class Camera
{
	private mat4 resultMatrix;
	private float width, height;
	public vec3 Position { get; set; }
	public float Scale { get; set; }
	public Camera(vec3 pos)
	{
		Position = pos;
	}
	private void UpdateMatrix()
	{
		resultMatrix = matOp.CreateOrthographic(width, height, -1000f, 10000f);
	}
	public void ViewResize(float width, float height)
	{
		this.width = width;
		this.height = height;
		UpdateMatrix();
	}
}
