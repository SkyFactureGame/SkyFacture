﻿using Silk.NET.OpenGL;
using System;

namespace SkyFacture.Graphics.Memory;

public class VertexArray : IDisposable
{
	public readonly uint Handle;

	public VertexArray()
	{
		Handle = Gl.GenVertexArray();
	}
	public void Bind()
		=> Gl.BindVertexArray(Handle);
	public unsafe void AttributePointer<T>(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offset) where T : unmanaged
	{
		Gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint)sizeof(T), (void*)(offset * sizeof(T)));
		Gl.EnableVertexAttribArray(index);
	}
	public void Dispose()
		=> Gl.DeleteVertexArray(Handle);
}
