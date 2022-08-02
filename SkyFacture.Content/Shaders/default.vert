#version 330 core

layout (location = 0) in vec2 vPos;

out vec2 fUV;
out vec4 fColor;

uniform sampler2D uTex;

void main() {
	gl_Position = vec4(vPos, 1, 1);
	fUV = vPos;
	fColor = vec4(1, 1, 1, 1);
}