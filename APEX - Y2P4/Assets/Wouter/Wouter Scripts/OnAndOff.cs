using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAndOff : MonoBehaviour {

    public List<GameObject> obs = new List<GameObject>();

    public void TurnOffObjects()
    {
        for (int i = 0; i < obs.Count; i++)
        {
            if (obs[i] != null)
            {
                obs[i].SetActive(false);
            }

        }
    }

    public void TurnOnObjects()
    {
        for (int i = 0; i < obs.Count; i++)
        {
            if (obs[i] != null)
            {
                obs[i].SetActive(true);
            }
        }
    }
}
