using System.Collections.Generic;
using UnityEngine;

public class PZ_HiddenKey : MonoBehaviour 
{

    [SerializeField]
    private Collider hiddenKeyCollider;

    [SerializeField]
    private List<GameObject> hints = new List<GameObject>();

    private void Awake()
    {
        hiddenKeyCollider.enabled = false;
    }

    public void StartPuzzle()
    {
        hiddenKeyCollider.enabled = true;

        for (int i = 0; i < hints.Count; i++)
        {
            hints[i].SetActive(true);
        }
    }

    public void CompletePuzzle()
    {
        hiddenKeyCollider.enabled = false;
    }
}
