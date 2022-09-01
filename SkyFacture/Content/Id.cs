using System;

namespace SkyFacture.Content;

public readonly struct Id : IEquatable<Id>
{
	public readonly string Namespace, Name;
	public Id(string @namespace, string name)
	{
		Namespace = @namespace;
		Name = name;
	}
	
	public override bool Equals(object? obj)
		=> obj is Id id
		&& Equals(id);
	public bool Equals(Id other)
		=> this.Namespace == other.Namespace
		&& this.Name == other.Name;
	public override string ToString()
		=> String.Concat(Namespace, ":", Name);
	public static Id Of(string @namespace, string name)
		=> new(@namespace, name);
	public static Id Of(string name)
		=> new(Const.DefaultNamespace, name);
	public static bool operator ==(Id left, Id right) => left.Equals(right);
	public static bool operator !=(Id left, Id right) => !left.Equals(right);

	public override int GetHashCode()
		=> HashCode.Combine(Namespace, Name);
}