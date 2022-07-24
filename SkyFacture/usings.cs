global using SkyFacture.Content;
global using System.Collections.Generic;
global using System.Linq;
global using mat4 = OpenTK.Mathematics.Matrix4;
global using vec2 = OpenTK.Mathematics.Vector2;
global using vec2i = OpenTK.Mathematics.Vector2i;
global using vec3 = OpenTK.Mathematics.Vector3;
global using vec4 = OpenTK.Mathematics.Vector4;
global using FBuff = SkyFacture.Drawing.Buffers.Buffer<float>;

public static class NumberExtensions
{
	public static uint FromCapacityToIndex(this uint index)
		=> index - 1;
	public static uint FromIndexToCapacity(this uint cap)
		=> cap + 1;

	//Unsign funcs
	public static unsafe ulong u(this long signedLong)
		=> *(ulong*)&signedLong;
	public static unsafe uint u(this int signedInt)
		=> *(uint*)&signedInt;
	public static unsafe ushort u(this short signedShort)
		=> *(ushort*)&signedShort;
	public static unsafe byte u(this sbyte signedByte)
		=> *(byte*)&signedByte;

	//Sign funcs
	public static unsafe long s(this ulong unsignedLong)
		=> *(long*)&unsignedLong;
	public static unsafe int s(this uint unsignedInt)
		=> *(int*)&unsignedInt;
	public static unsafe short s(this ushort unsignedShort)
		=> *(short*)&unsignedShort;
	public static unsafe sbyte s(this byte unsignedByte)
		=> *(sbyte*)&unsignedByte;
}