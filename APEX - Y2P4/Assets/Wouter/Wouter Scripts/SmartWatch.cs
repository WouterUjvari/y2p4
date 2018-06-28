using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartWatch : MonoBehaviour
{

    public bool enableWaypoint;
    public GameObject waypointArrow;
    public Transform target;
    public GameObject tipElement;
    
    public List<GameObject> listOfTips = new List<GameObject>();
    
    public void ToggleTips()
    {
        tipElement.SetActive(tipElement.activeInHierarchy ? false : true);

        for (int i = 0; i < listOfTips.Count; i++)
        {
            listOfTips[i].SetActive(false);
        }

        listOfTips[FlowManager.instance.currentPuzzle].SetActive(true);
    }
}
