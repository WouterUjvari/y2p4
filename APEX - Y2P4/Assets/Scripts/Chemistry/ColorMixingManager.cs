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
        Color mixedColor = new Color();
        string mixedColorName = null;

        string toMixName = null;

        for (int i = 0; i < colors.Count; i++)
        {
            if (colors[i].color == toMix)
            {
                toMixName = colors[i].name;
            }
        }

        switch (myColorName)
        {
            case "red":

                switch (toMixName)
                {
                    case "red":

                        mixedColorName = "red";
                        break;

                    case "blue":

                        mixedColorName = "purple";
                        break;
                    case "yellow":

                        mixedColorName = "purple";
                        break;
                    case "green":

                        mixedColorName = "brown";
                        break;
                    case "purple":

                        mixedColorName = "yellow";
                        break;
                    case "brown":

                        mixedColorName = "blue";
                        break;
                }
                break;
            case "blue":

                switch (toMixName)
                {
                    case "red":

                        mixedColorName = "purple";
                        break;

                    case "blue":

                        mixedColorName = "blue";
                        break;
                    case "yellow":

                        mixedColorName = "brown";
                        break;
                    case "green":

                        mixedColorName = "yellow";
                        break;
                    case "purple":

                        mixedColorName = "red";
                        break;
                    case "brown":

                        mixedColorName = "purple";
                        break;
                }
                break;
            case "yellow":

                switch (toMixName)
                {
                    case "red":

                        mixedColorName = "purple";
                        break;

                    case "blue":

                        mixedColorName = "purple";
                        break;
                    case "yellow":

                        mixedColorName = "yellow";
                        break;
                    case "green":

                        mixedColorName = "blue";
                        break;
                    case "purple":

                        mixedColorName = "green";
                        break;
                    case "brown":

                        mixedColorName = "green";
                        break;
                }
                break;
            case "green":

                switch (toMixName)
                {
                    case "red":

                        mixedColorName = "purple";
                        break;

                    case "blue":

                        mixedColorName = "brown";
                        break;
                    case "yellow":

                        mixedColorName = "red";
                        break;
                    case "green":

                        mixedColorName = "green";
                        break;
                    case "purple":

                        mixedColorName = "blue";
                        break;
                    case "brown":

                        mixedColorName = "red";
                        break;
                }
                break;
            case "purple":

                switch (toMixName)
                {
                    case "red":

                        mixedColorName = "yellow";
                        break;

                    case "blue":

                        mixedColorName = "red";
                        break;
                    case "yellow":

                        mixedColorName = "blue";
                        break;
                    case "green":

                        mixedColorName = "brown";
                        break;
                    case "purple":

                        mixedColorName = "purple";
                        break;
                    case "brown":

                        mixedColorName = "yellow";
                        break;
                }
                break;
            case "brown":

                switch (toMixName)
                {
                    case "red":

                        mixedColorName = "blue";
                        break;

                    case "blue":

                        mixedColorName = "purple";
                        break;
                    case "yellow":

                        mixedColorName = "red";
                        break;
                    case "green":

                        mixedColorName = "purple";
                        break;
                    case "purple":

                        mixedColorName = "yellow";
                        break;
                    case "brown":

                        mixedColorName = "brown";
                        break;
                }
                break;
        }

        for (int i = 0; i < colors.Count; i++)
        {
            if (colors[i].name == mixedColorName)
            {
                mixedColor = colors[i].color;
            }
        }

        return mixedColor;
    }
}
