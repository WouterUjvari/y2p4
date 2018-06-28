using System.Collections.Generic;
using UnityEngine;

public class PZ_HiddenKey : Puzzle 
{

    [SerializeField]
    private Collider hiddenKeyCollider;

    [SerializeField]
    private List<GameObject> hints = new List<GameObject>();

    private void Awake()
    {
        TogglePuzzleItems(false);
    }

    public override void StartPuzzle()
    {
        FlowManager.instance.NextShipAIVoice(0);
        FlowManager.instance.NextAnouncerVoiceTimer(4);
        TogglePuzzleItems(true);
    }

    private void TogglePuzzleItems(bool b)
    {
        hiddenKeyCollider.enabled = b;

        for (int i = 0; i < hints.Count; i++)
        {
            hints[i].SetActive(b);
        }
    }

    public override void CompletePuzzle()
    {
        FlowManager.instance.NextShipAIVoice(0);
        FlowManager.instance.NextAnouncerVoice(4);
        FlowManager.instance.NextPuzzle(15);

        hiddenKeyCollider.enabled = false;
    }
}
