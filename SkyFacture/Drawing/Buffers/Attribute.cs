using OpenTK.Graphics.OpenGL;

namespace SkyFacture.Drawing.Buffers;

public ref struct Attribute
{
	/// <summary>
	/// Attribute position (layout position)
	/// </summary>
	public readonly int attrHandle;
	/// <summary>
	/// Attribute <i>Unit</i> type
	/// </summary>
	public readonly VertexAttribPointerType type;
	/// <summary>
	/// Attribute size (in units)
	/// </summary>
	public readonly int size;
	/// <summary>
	/// Normalized...
	/// </summary>
	public readonly bool normalized;
	/// <summary>
	/// Vertex size
	/// </summary>
	public readonly int stride;
	/// <summary>
	/// Size of previous attributes
	/// </summary>
	public readonly int offset;
	public Attribute(int attrHandle, VertexAttribPointerType type, int size, bool normalized, int stride, int offset)
	{
		this.stride = stride;
		this.attrHandle = attrHandle;
		this.type = type;
		this.size = size;
		this.normalized = normalized;
		this.offset = offset;
	}
}
