#version 330 core

in vec2 fTexPos;

out vec4 FragColor;

uniform sampler2D tex;
uniform vec4 color = vec4(1f, 1f, 1f, 1f);

void main()
{
    FragColor = texture(tex, fTexPos) * color;
} 