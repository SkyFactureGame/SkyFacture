﻿#version 330 core

layout (location = 0) in vec2 vPos;
layout (location = 1) in vec2 vUV;

uniform mat4 matrix;
uniform vec4 color = vec4(1);

out vec2 fUV;
out vec4 fColor;

void main()
{
	gl_Position = matrix * vec4(vPos.xy, 1, 1);
	fUV = vUV;
	fColor = color;
}
