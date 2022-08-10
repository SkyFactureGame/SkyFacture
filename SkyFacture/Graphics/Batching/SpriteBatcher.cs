using SkyFacture.Graphics.Shaders;
using SkyFacture.Graphics.Textures;
using System;

namespace SkyFacture.Graphics.Batching;
public class SpriteBatcher
{
	private readonly ShaderProgram shader;
	private const uint MaxRequest = 1000;

	public bool InvertedZSort { get; set; } = true;

	public SpriteBatcher(ShaderProgram shader)
	{
		this.shader = shader;
	}
	public void Dispose()
	{

	}
	public void Draw(SpriteRegion region, vec2 pos, vec2 size)
	{

	}
	private float lastZ = -1f;
	public void ZLayer(float z)
	{
		lastZ = InvertedZSort ? -z : z;
	}
	public void Flush()
	{

	}
}
