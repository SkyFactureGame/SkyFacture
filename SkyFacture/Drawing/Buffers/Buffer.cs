using OpenTK.Graphics.OpenGL;
using System;
using System.Runtime.InteropServices;

namespace SkyFacture.Drawing.Buffers;
public class Buffer<T> : GLObj where T : unmanaged
{
	public readonly BufferTarget target;
	private readonly int elementSize;
	private int elementCount;
	public T[] Content
	{
		get
		{
			T[] items = new T[elementCount];
			GL.BindBuffer(BufferTarget.ArrayBuffer, handle);
			GL.GetBufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, (IntPtr)(elementSize * elementCount), items);
			return items;
		}
	}
	public Buffer(BufferTarget target, int elementSize) : base(GL.GenBuffer())
	{
		this.target = target;
		this.elementSize = elementSize;
	}
	public Buffer<T> Bind()
	{
		GL.BindBuffer(target, handle);
		return this;
	}
	public void Init(int size, int elementCount, T[] data, BufferUsageHint usage = BufferUsageHint.StaticDraw)
	{
		this.elementCount = elementCount;
		if (size != elementCount * elementSize)
			throw new ArgumentException("Invalid size");

		GL.BufferData(target, size, data, usage);
	}
}
