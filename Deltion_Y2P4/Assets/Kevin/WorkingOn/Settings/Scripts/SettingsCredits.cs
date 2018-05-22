using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsCredits : MonoBehaviour {

	public GameObject creditsPannel;

	public GameObject titelPannel;
	public void ToggleMainScreen()
	{
		if(titelPannel.activeSelf == true)
		{
			titelPannel.SetActive(false);
			creditsPannel.SetActive(true);
		}
		else if(creditsPannel.activeSelf == true)
		{
			creditsPannel.SetActive(true);
			titelPannel.SetActive(true);
		}
	}
}
