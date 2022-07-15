#version 330 core

in vec2 fTexPos;
in vec4 fColor;

out vec4 FragColor;

uniform sampler2D tex;

void main()
{
    FragColor = texture(tex, fTexPos) * fColor;
} 