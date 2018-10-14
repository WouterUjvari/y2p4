using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipScreen : MonoBehaviour 
{

    private GameObject currentTip;

    [SerializeField]
    private List<GameObject> tips = new List<GameObject>();
    [SerializeField]
    private float nextTipSpeed = 1f;
    [SerializeField]
    private Image nextTipTimerFill;

    private void Awake()
    {
        for (int i = 0; i < tips.Count; i++)
        {
            tips[i].SetActive(false);
        }

        currentTip = tips[Random.Range(0, tips.Count)];
        currentTip.SetActive(true);
    }

    private void Update()
    {
        if (nextTipTimerFill.fillAmount < 1)
        {
            nextTipTimerFill.fillAmount += Time.deltaTime * nextTipSpeed;
        }
        else
        {
            NextTip();
            nextTipTimerFill.fillAmount = 0;
        }
    }

    private void NextTip()
    {
        if (tips.Count <= 1)
        {
            return;
        }

        currentTip.SetActive(false);

        GameObject nextTip = null;
        while (!nextTip || nextTip == currentTip)
        {
            nextTip = tips[Random.Range(0, tips.Count)];
        }

        currentTip = nextTip;
        currentTip.SetActive(true);
    }
}
