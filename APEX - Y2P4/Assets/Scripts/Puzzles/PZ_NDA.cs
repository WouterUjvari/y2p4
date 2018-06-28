﻿using UnityEngine;

public class PZ_NDA : Puzzle
{

    private bool signedNDA;

    private bool isSigning;
    public Drone drone;

    public override void StartPuzzle()
    {
        ExtraDroneFunctionality.instance.ToggleDroneCam(false);
        FlowManager.instance.NextAnouncerVoice(0);
        ExtraDroneFunctionality.instance.itemIndex = 1;
        ExtraDroneFunctionality.instance.anim.SetTrigger("GiveItem");
        drone.GoLookAtPlayer();
    }

    public override void CompletePuzzle()
    {
        drone.GetNewState();
        ExtraDroneFunctionality.instance.anim.SetTrigger("Retract");
        Destroy(ExtraDroneFunctionality.instance.itemInHand, 0.1f);
        FlowManager.instance.NextAnouncerVoice(4);
        FlowManager.instance.NextPuzzle(10);
    }

    public void IsSigning(bool b)
    {
        if (isSigning && !b)
        {
            if (!signedNDA)
            {
                CompletePuzzle();
                signedNDA = true;
            }
        }

        isSigning = b ? true : false;
    }
}
