using SkyFacture.Graphics.Textures;

namespace SkyFacture.Graphics.Batching;

public record struct DrawRequest(Region sprite, vec3 pos, vec2 size, vec3 rotation, vec4 color);