using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour 
{

    public static TimeManager instance;

    public static float currentTime;

    public bool pausedTime = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        currentTime += pausedTime ? 0 : Time.deltaTime;
    }

    public void ChangeTime(float seconds)
    {
        currentTime += seconds;
    }
}
