using UnityEngine;

public static class GradientConstructor
{
    public static Gradient GetGradient (Color start, Color end)
    {
        Gradient gradient = new Gradient();
        gradient.colorKeys = new[]
        {
            new GradientColorKey(start, 0),
            new GradientColorKey(end, 1)
        };
        gradient.alphaKeys = new[]
        {
            new GradientAlphaKey(1, 0),
            new GradientAlphaKey(1, 1)
        };
        return gradient;
    }
} 
