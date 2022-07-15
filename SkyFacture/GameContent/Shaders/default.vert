#version 330 core

layout (location = 0) in vec2 vPos;
layout (location = 1) in vec2 vTexPos;
layout (location = 2) in vec4 vColor;

uniform mat4 matrix;

out vec2 fTexPos;
out vec4 fColor;

void main()
{
	gl_Position = matrix * vec4(vPos.xy, 1, 1);
	fTexPos = vTexPos;
	fColor = vColor;
}
