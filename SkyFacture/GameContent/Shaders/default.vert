#version 330 core

layout (location = 0) in vec2 vPos;
layout (location = 1) in vec2 vTexPos;
layout (location = 2) in int vColor;

uniform mat4 matrix;

out vec2 fTexPos;
out vec4 fColor;

vec4 unpackColor(int color) 
{
	float r = (color & 0xFF) / 255f;
	float g = ((color >> 8) & 0xFF) / 255f;
	float b = ((color >> 16) & 0xFF) / 255f;
	float a = ((color >> 24) & 0xFF) / 255f;
	return vec4(1, 1, 1, 1);
}

void main()
{
	gl_Position = matrix * vec4(vPos.xy, 1, 1);
	fTexPos = vTexPos;
	fColor = unpackColor(vColor);
}
