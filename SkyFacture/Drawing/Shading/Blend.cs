using OpenTK.Graphics.OpenGL;
using System;

namespace SkyFacture.Drawing.Shading;

/// <summary>
/// Changes the type of blending
/// </summary>
public class Blend : IGLGlobalFunc, IEquatable<Blend>
{
	public readonly BlendingFactorSrc ColorBlendingSrc, AlphaBlendingSrc;
	public readonly BlendingFactorDest ColorBlendingDest, AlphaBlendingDest;
	public readonly BlendEquationMode Mode;
	public Blend(BlendingFactorSrc src, BlendingFactorDest dest, BlendEquationMode mode) : this(src, dest, src, dest, mode) { }
	public Blend(BlendingFactorSrc src, BlendingFactorDest dest) : this(src, dest, src, dest) { }
	public Blend(BlendingFactorSrc colorSrc, BlendingFactorDest colorDest, BlendingFactorSrc alphaSrc, BlendingFactorDest alphaDest) : this(colorSrc, colorDest, alphaSrc, alphaDest, BlendEquationMode.FuncAdd) { }
	public Blend(BlendingFactorSrc colorSrc, BlendingFactorDest colorDest, BlendingFactorSrc alphaSrc, BlendingFactorDest alphaDest, BlendEquationMode mode)
	{
		this.ColorBlendingSrc = colorSrc;
		this.ColorBlendingDest = colorDest;
		this.AlphaBlendingSrc = alphaSrc;
		this.AlphaBlendingDest = alphaDest;
		this.Mode = mode;
	}
	public override bool Equals(object? other)
		=> other is Blend blend && Equals(blend);
	public bool Equals(Blend? other)
		=> other is not null
		&& this.ColorBlendingSrc == other.ColorBlendingSrc
		&& this.AlphaBlendingSrc == other.AlphaBlendingSrc
		&& this.ColorBlendingDest == other.ColorBlendingDest
		&& this.AlphaBlendingDest == other.AlphaBlendingDest
		&& this.Mode == other.Mode;
	public override int GetHashCode()
		=> HashCode.Combine(this.ColorBlendingSrc, this.ColorBlendingDest, this.AlphaBlendingSrc, this.AlphaBlendingDest);
	public void Use()
	{
		GL.BlendFuncSeparate(this.ColorBlendingSrc, this.ColorBlendingDest, this.AlphaBlendingSrc, this.AlphaBlendingDest);
		GL.BlendEquation(this.Mode);
	}

	public static readonly Blend Default, Difference, Lighten, Darken, Negative, Additive;
	static Blend()
	{
		Default = new(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha, BlendEquationMode.FuncAdd);
		Difference = new(BlendingFactorSrc.DstColor, BlendingFactorDest.Zero, BlendEquationMode.FuncSubtract);
		Lighten = new(BlendingFactorSrc.One, BlendingFactorDest.One, BlendEquationMode.Max);
		Darken = new(BlendingFactorSrc.One, BlendingFactorDest.One, BlendEquationMode.Min);
		Negative = new(BlendingFactorSrc.SrcColor, BlendingFactorDest.OneMinusSrcColor, BlendEquationMode.FuncReverseSubtract);
		Additive = new(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.One, BlendEquationMode.FuncAdd);
	}
	public static bool operator ==(Blend left, Blend right)
		=> left.Equals(right);
	public static bool operator !=(Blend left, Blend right)
		=> !left.Equals(right);
	public static void Enable()
	{
		GL.Enable(EnableCap.Blend);
		Default.Use();
	}
	public static void Disable()
		=> GL.Disable(EnableCap.Blend);
}
