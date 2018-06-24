using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour 
{

    [SerializeField]
    private List<Interactable> lockables = new List<Interactable>();

    public void ToggleLock(bool b)
    {
        for (int i = 0; i < lockables.Count; i++)
        {
            lockables[i].Lock(b);
        }
    }
}
