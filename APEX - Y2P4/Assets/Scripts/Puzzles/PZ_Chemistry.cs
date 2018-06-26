using System.Collections;
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

    private void Awake()
    {
        for (int i = 0; i < interactablesToLockAtStart.Count; i++)
        {
            interactablesToLockAtStart[i].Lock(true);
        }
    }

    private void GetNewColorToMix()
    {
        currentColorToMix = ColorMixingManager.instance.colors[Random.Range(0, ColorMixingManager.instance.colors.Count)];
        mixingColor++;
    }

    public override void StartPuzzle()
    {
        FlowManager.instance.nextShipAIVoice();
        GetNewColorToMix();
        UnlockLocks();
    }

    private void UnlockLocks()
    {
        for (int i = 0; i < locks.Count; i++)
        {
            locks[i].Lock(false);
        }
    }

    public override void CompletePuzzle()
    {
        FlowManager.instance.nextPuzzle();
    }
}
