using OpenTK.Graphics.OpenGL;
using SkyFacture.Drawing.Shading;
using System;

namespace SkyFacture.Drawing.Buffers;

//TODO: Move Attributes сюда
public class VertexArray : GLObj, IDisposable
{
	private readonly Shader shader;
	public VertexArray(Shader shader) : base(GL.GenVertexArray())
	{
		this.shader = shader;
		Bind();
	}
	public void BindAttribute<T>(Attribute attr, Buffer<T> buff) where T : unmanaged
	{
		buff.Bind();
		GL.EnableVertexAttribArray(attr.attrHandle);
		GL.VertexAttribPointer(attr.attrHandle, attr.size, attr.type, attr.normalized, attr.stride, attr.offset);
	}
	public void Bind()
	{
		GL.BindVertexArray(handle);
		shader.Bind();
	}
	public void Dispose()
	{
		GL.DeleteVertexArray(handle);
	}
	~VertexArray()
		=> Dispose();
	public void Draw(PrimitiveType type, int first, int count)
	{
		shader.Use();
		GL.DrawArrays(type, first, count);
	}
}
