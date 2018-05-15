using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour 
{

    [SerializeField]
    private TextMeshProUGUI timeText;

    private void Update()
    {
        if (timeText != null)
        {
            string minutes = Mathf.Floor(TimeManager.currentTime / 60).ToString("00");
            string seconds = Mathf.Floor(TimeManager.currentTime % 60).ToString("00");

            timeText.text = minutes + ":" + seconds;
        }
    }
}
