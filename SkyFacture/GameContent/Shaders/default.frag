#version 330 core

in vec2 fUV;
in vec4 fColor;

out vec4 FragColor;

uniform sampler2D tex;

vec4 toGrayscale(in vec4 color)
{
    float average = (color.r + color.g + color.b) / 3.0;
    return vec4(average, average, average, 1.0);
}

vec4 colorize(in vec4 grayscale, in vec4 color)
{
    return (grayscale * color);
}

void main()
{
    vec4 textureColor = texture(tex, fUV);

    if (fColor.xyz == vec3(1, 1, 1)) //White
    {
        FragColor = textureColor;
    }
    else
    {
        vec4 gs = toGrayscale(textureColor);
        FragColor = colorize(gs, fColor);
    }
    FragColor.a = textureColor.a;
}