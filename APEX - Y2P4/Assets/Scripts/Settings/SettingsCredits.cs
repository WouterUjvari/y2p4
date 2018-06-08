using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsCredits : MonoBehaviour {

	public GameObject creditsPannel;

	public GameObject titelPannel;
	public void ToggleMainScreen()
	{
		if(titelPannel.activeSelf)
		{
			titelPannel.SetActive(false);
			creditsPannel.SetActive(true);
		}
		else if(creditsPannel.activeSelf)
		{
			creditsPannel.SetActive(false);
			titelPannel.SetActive(true);
		}
	}
}
