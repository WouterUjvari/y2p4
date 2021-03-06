﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PZ_Chemistry : Puzzle 
{

    private int mixingColor;

    [SerializeField]
    private ColorMixingManager.Colors currentColorToMix;
    [SerializeField]
    private List<Interactable> locks = new List<Interactable>();
    [SerializeField]
    private List<Interactable> interactablesToLockAtStart = new List<Interactable>();
    [SerializeField]
    private ObjectSnapSpot checkCorrectColorStandSnapspot;
    [SerializeField]
    private Animator checkCorrectColorStandAnim;
    [SerializeField]
    private Material colorToMakeIndicator;

    private void Awake()
    {
        currentColorToMix = new ColorMixingManager.Colors
        {
            name = "null"
        };
        ShowColorToMake(Color.white);

        for (int i = 0; i < interactablesToLockAtStart.Count; i++)
        {
            if (interactablesToLockAtStart[i] != null)
            {
                interactablesToLockAtStart[i].Lock = true;
            }
        }
    }

    private void GetNewColorToMix()
    {
        currentColorToMix = ColorMixingManager.instance.colors[Random.Range(0, ColorMixingManager.instance.colors.Count)];
        ShowColorToMake(currentColorToMix.color);
        mixingColor++;
    }

    public void CheckCorrectColor()
    {
        if (currentColorToMix.name == "null")
        {
            return;
        }

        Flask flask = checkCorrectColorStandSnapspot.snappedObject.GetComponentInChildren<Flask>();

        if (flask.myColorName == currentColorToMix.name)
        {
            checkCorrectColorStandAnim.SetTrigger("Correct");

            if (mixingColor == 1)
            {
                GetNewColorToMix();
            }
            else
            {
                CompletePuzzle();

                currentColorToMix = new ColorMixingManager.Colors
                {
                    name = "null"
                };
                ShowColorToMake(Color.white);
            }
        }
        else
        {
            checkCorrectColorStandAnim.SetTrigger("False");
        }
    }

    public override void StartPuzzle()
    {
        FlowManager.instance.StartChemical();
        GetNewColorToMix();
        UnlockLocks();
    }

    private void UnlockLocks()
    {
        for (int i = 0; i < locks.Count; i++)
        {
            if (locks[i] != null)
            {
                locks[i].Lock = false;
            }
        }
    }

    public override void CompletePuzzle()
    {
        FlowManager.instance.CompleteChemical();
    }

    public void ShowColorToMake(Color toMake)
    {
        if (colorToMakeIndicator)
        {
            colorToMakeIndicator.SetColor("_EmissionColor", toMake * 3f);
        }
    }
}
