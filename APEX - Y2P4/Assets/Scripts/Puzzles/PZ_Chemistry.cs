using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PZ_Chemistry : MonoBehaviour 
{

    [SerializeField]
    private ColorMixingManager.Colors currentColorToMix;

    private void GetNewColorToMix()
    {
        currentColorToMix = ColorMixingManager.instance.colors[Random.Range(0, ColorMixingManager.instance.colors.Count)];
    }

    public void StartPuzzle()
    {
        GetNewColorToMix();
    }
}
