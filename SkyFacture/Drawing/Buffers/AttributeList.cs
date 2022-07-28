using OpenTK.Graphics.OpenGL;
using System;

namespace SkyFacture.Drawing.Buffers;
public class AttributeList
{
	public readonly u8 Size;
	private readonly i32[] buffers; 
	public AttributeList(u8 size)
	{
		Size = size;
		buffers = new int[size];
	}
	public void SafeBind<T>(Attribute attr, Buffer<T> buffer) where T : unmanaged
	{
		if (!Binded(attr.attrHandle, buffer))
		{
			buffers[attr.attrHandle] = buffer.Handle;
			buffer.Bind();
			GL.EnableVertexAttribArray(attr.attrHandle);
			GL.VertexAttribPointer(attr.attrHandle, attr.size, attr.type, attr.normalized, attr.stride, attr.offset);
		}
	}
	public bool Binded<T>(i32 attrLoc, Buffer<T> buffer) where T : unmanaged 
	{
		i32 bindedBuff = buffers[attrLoc];
		return bindedBuff == buffer.Handle;
	}
	public void Bind<T>(i32 attrLoc, Buffer<T> buffer) where T : unmanaged
	{
		buffers[attrLoc] = buffer.Handle;
	}
}
