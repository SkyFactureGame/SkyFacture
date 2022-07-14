// The NiTiS-Dev licenses this file to you under the MIT license.

using System;
using YamlDotNet.Serialization;

namespace SkyFacture.Drawing.Sprites;

[Serializable]
public class AliasFile
{
	[YamlMember(Alias = "blending", ApplyNamingConventions = false)]
	public bool Blending { get; set; } = true;
	[YamlMember(Alias = "textures", ApplyNamingConventions = false)]
	public TextureRegion[] Textures { get; set; } = Array.Empty<TextureRegion>();
}
[Serializable]
public class TextureRegion
{
	[YamlMember(Alias = "name", ApplyNamingConventions = false)]
	public string? Name { get; set; }
	[YamlMember(Alias = "size", ApplyNamingConventions = false)]
	public string? Size { get; set; }
	[YamlMember(Alias = "pos", ApplyNamingConventions = false)]
	public string? Position { get; set; }
}
