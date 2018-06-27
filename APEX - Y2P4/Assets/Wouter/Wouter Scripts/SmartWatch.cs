using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartWatch : MonoBehaviour
{

    public bool enableWaypoint;
    public GameObject waypointArrow;
    public Transform target;
    public bool tipsOn;
    public GameObject tipElement;
    
    public List<GameObject> listOfTips = new List<GameObject>();
    


    void FixedUpdate()
    {
        waypointArrow.SetActive(enableWaypoint);
        if (enableWaypoint)
        {
            waypointArrow.transform.LookAt(target);
        }
    }

    public void ToggleTips()
    {
        tipsOn = !tipsOn;
        tipElement.SetActive(tipsOn); 

        listOfTips[0].SetActive(false);
        listOfTips[1].SetActive(false);
        listOfTips[2].SetActive(false);
        listOfTips[3].SetActive(false);
        listOfTips[4].SetActive(false);
        listOfTips[5].SetActive(false);

        listOfTips[FlowManager.instance.currentPuzzle].SetActive(true);
    }
}
