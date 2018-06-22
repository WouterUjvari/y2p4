using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMixingManager : MonoBehaviour
{

    public static ColorMixingManager instance;

    public List<Colors> colors = new List<Colors>();

    [System.Serializable]
    public struct Colors
    {
        public Color color;
        public string name;
        public string burnsIntoColor;
        public List<ColorMix> colorMixingOptions;

        [System.Serializable]
        public struct ColorMix
        {
            public string colorToMixWith;
            public string resultColor;
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public Colors GetColorByName(string name)
    {
        for (int i = 0; i < colors.Count; i++)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new Colors();
            }

            if (colors[i].name == name)
            {
                return colors[i];
            }
        }

        return new Colors();
    }

    public Color GetMixedColor(string myColorName, Color toMix)
    {
        // Setting the default color to the color the player is mixing.
        Color mixedColor = new Color();
        for (int i = 0; i < colors.Count; i++)
        {
            if (colors[i].name == myColorName)
            {
                mixedColor = colors[i].color;
            }
        }

        string mixedColorName = null;
        string toMixName = null;

        // Getting the name of the color we have to mix.
        for (int i = 0; i < colors.Count; i++)
        {
            if (colors[i].color == toMix)
            {
                toMixName = colors[i].name;
            }
        }

        // Finding the mixedColorName.
        for (int i = 0; i < colors.Count; i++)
        {
            if (colors[i].name == myColorName)
            {
                for (int ii = 0; ii < colors[i].colorMixingOptions.Count; ii++)
                {
                    if (colors[i].colorMixingOptions[ii].colorToMixWith == toMixName)
                    {
                        mixedColorName = colors[i].colorMixingOptions[ii].resultColor;
                        break;
                    }
                }
            }
        }

        // Converting the mixedColorName to an actual color.
        for (int i = 0; i < colors.Count; i++)
        {
            if (colors[i].name == mixedColorName)
            {
                mixedColor = colors[i].color;
            }
        }

        return mixedColor;
    }

    public Color GetBurnedColor(string toBurn)
    {
        Color burnedColor = new Color();
        string burnedColorName = null;

        for (int i = 0; i < colors.Count; i++)
        {
            if (colors[i].name == toBurn)
            {
                burnedColor = colors[i].color;
                burnedColorName = colors[i].burnsIntoColor;
                break;
            }
        }

        if (!string.IsNullOrEmpty(burnedColorName))
        {
            for (int i = 0; i < colors.Count; i++)
            {
                if (colors[i].name == burnedColorName)
                {
                    burnedColor = colors[i].color;
                }
            }
        }

        return burnedColor;
    }
}
