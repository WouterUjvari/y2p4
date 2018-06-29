using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipUXManager : MonoBehaviour {

    public List<GameObject> tipsUX = new List<GameObject>();
    public int tipIndex;


    void Start()
    {
        SpawnNewTip();
    }

    public void SpawnNewTip()
    {
        for (int i = 0; i < tipsUX.Count; i++)
        {
            if(tipsUX[i] != null)
            {
                tipsUX[i].SetActive(false);
            }
            
        }
        tipsUX[tipIndex].SetActive(true);
    }
}
