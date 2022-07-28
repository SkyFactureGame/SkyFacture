using OpenTK.Graphics.OpenGL;
using SkyFacture.Drawing.Shading;
using System;

namespace SkyFacture.Drawing.Buffers;

//TODO: Move Attributes сюда
public class VertexArray : GLObj, IDisposable
{
	private readonly AttributeList attributes;
	public VertexArray(u8 attrSize) : base(GL.GenVertexArray())
	{
		BindValidator.Bind(BindTarget.VertexArray, this.Handle);
		GL.BindVertexArray(Handle);
		attributes = new(attrSize);
	}
	public void BindAttribute<T>(Attribute attr, Buffer<T> buff) where T : unmanaged
	{
		attributes.SafeBind(attr, buff);
	}
	public void HardBindAttribute<T>(Attribute attr, Buffer<T> buff) where T : unmanaged
	{
		buff.Bind();
		GL.EnableVertexAttribArray(attr.attrHandle);
		GL.VertexAttribPointer(attr.attrHandle, attr.size, attr.type, attr.normalized, attr.stride, attr.offset);
	}
	public void Bind()
	{
		if (BindValidator.ShoudBind(BindTarget.VertexArray, Handle))
			GL.BindVertexArray(Handle);
	}
	public void Dispose()
	{
		GL.DeleteVertexArray(Handle);
	}
	~VertexArray()
		=> Dispose();
	public void Draw(PrimitiveType type, int first, int count)
	{
		GL.DrawArrays(type, first, count);
	}
}
