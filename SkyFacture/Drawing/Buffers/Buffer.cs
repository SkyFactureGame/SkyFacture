using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SkyFacture.Drawing.Buffers;

[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
public class Buffer<T> : GLObj where T : unmanaged
{
	public readonly BufferTarget target;
	private readonly int elementSize;
	private int elementCount, size;
	public Buffer(BufferTarget target, int elementSize) : base(GL.GenBuffer())
	{
		this.target = target;
		this.elementSize = elementSize;
		Bind();
	}
	public Buffer<T> Bind()
	{
		GL.BindBuffer(target, Handle);
		return this;
	}
	/// <summary>
	/// Initialize buffer
	/// </summary>
	/// <param name="size">Buffer size (in bytes)</param>
	/// <param name="elementCount">Amount of drawing vertexes</param>
	/// <param name="data">Buffer data</param>
	/// <exception cref="ArgumentException"></exception>
	public void Init(int size, int elementCount, T[] data, BufferUsageHint usage = BufferUsageHint.StaticDraw)
	{
		this.elementCount = elementCount;
		this.size = size;
		if (size < elementCount * elementSize)
			throw new ArgumentException("Invalid size");

		GL.BufferData(target, size, data, usage);
	}
	public unsafe Attribute CreateAttribute(int handle, VertexAttribPointerType type)
		=> new(handle, type, size / elementCount / sizeof(T), false, elementSize, 0);
	public override string ToString()
		=> $"{{{target}<{typeof(T).Name}> [es:{elementSize} * ec:{elementCount}]}}";
}
