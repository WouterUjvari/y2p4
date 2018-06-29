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
        FlowManager.instance.StartLight();
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
        FlowManager.instance.CompleteLight();
        hiddenKeyCollider.enabled = false;
    }
}
