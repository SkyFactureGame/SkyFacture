// The NiTiS-Dev licenses this file to you under the MIT license.

using SkyFacture.Content.Abstractions;
using System;

namespace SkyFacture.Content.Registry;
public readonly struct AwaitedContent<TContent> where TContent : IContentType 
{
	private readonly bool byID = true;
	private readonly uint id = 0;
	public TContent? Get()
	{
		return byID ? Registrator.Get<TContent>(id) : default;
	}
	public AwaitedContent(uint id)
	{
		this.byID = true;
		this.id = id;
	}
	public static AwaitedContent<TContent> FromID(uint id)
		=> new(id);
	public static TContent operator ~(AwaitedContent<TContent> self)
		=> self.Get()!;
}
