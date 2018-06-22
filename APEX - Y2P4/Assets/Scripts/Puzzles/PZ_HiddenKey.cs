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
        hiddenKeyCollider.enabled = false;
    }

    public override void StartPuzzle()
    {
        hiddenKeyCollider.enabled = true;

        for (int i = 0; i < hints.Count; i++)
        {
            hints[i].SetActive(true);
        }
    }

    public override void CompletePuzzle()
    {
        hiddenKeyCollider.enabled = false;
    }
}
