using System;
using System.Diagnostics;
using Silk.NET.OpenGL;

namespace SkyFacture.Graphics.Memory;

/// <summary>
/// Memory allocated on Graphic card
/// </summary>
[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
public class Buffer<T> : IDisposable where T : unmanaged
{
	public readonly uint Handle;
	public readonly BufferTargetARB Target;
	public Buffer(BufferTargetARB target)
	{
		Target = target;
		Handle = Gl.GenBuffer();
	}
	public void Bind()
		=> Gl.BindBuffer(Target, Handle);
	public unsafe void Data(uint size, T[] data, BufferUsageARB usage = BufferUsageARB.StaticDraw)
	{
		fixed (void* ptr = data)
		{
			Gl.BufferData(Target, size, ptr, usage);
		}
	}
	public unsafe void Data(uint size, void* data, BufferUsageARB usage = BufferUsageARB.StaticDraw)
	{
		Gl.BufferData(Target, size, data, usage);
	}
	public unsafe void SubData(nint offset, nuint size, T[] data)
	{
		fixed (void* ptr = data)
		{
			Gl.BufferSubData(Target, offset, size, ptr);
		}
	}
	public unsafe void SubData(nint offset, nuint size, void* data)
	{
		Gl.BufferSubData(Target, offset, size, data);
	}
	public unsafe void SubData(nint offset, T[] data)
	{
		fixed (void* ptr = data)
		{
			Gl.BufferSubData(Target, offset, (nuint)(sizeof(T) * data.Length), ptr);
		}
	}
	public void Dispose()
		=> Gl.DeleteBuffer(Handle);
	public override string ToString()
		=> $"{{Buffer<{Target}>({Handle})}}";
}
