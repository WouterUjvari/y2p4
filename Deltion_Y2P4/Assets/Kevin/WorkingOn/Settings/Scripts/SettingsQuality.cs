using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsQuality : MonoBehaviour {


	private Vector3 handelMin;

	private float resultStep;

	public float maxMovement;

	private float lastStep;
	public Slider qualitySlider;

	public int qualityIndex = 5;

	void Start()
	{
		qualitySlider.maxValue = maxMovement - 0.01f;
		handelMin = transform.position;
		resultStep = maxMovement / 5;
	}

	void Update()
	{
		MovingHandel();
	}

	public void MovingHandel()
	{
		float handelValue;
		handelValue = Vector3.Distance(handelMin, transform.position);
		qualitySlider.value = 0.39f - handelValue;
		print(handelValue);
		if(handelValue >= lastStep + resultStep)
		{
			lastStep += resultStep;
			qualityIndex -= 1;
		}
		if(handelValue <= lastStep - resultStep)
		{
			lastStep -= resultStep;
			qualityIndex += 1;
		}
		if(handelValue >= 0.39f && qualityIndex == 1)
		{
			lastStep += resultStep;
			qualityIndex -= 1;
		}
		if(handelValue <= 0.01f && qualityIndex == 4)
		{
			lastStep -= resultStep;
			qualityIndex += 1;
		}
		ChangeQuality(qualityIndex);
	}

	public void ChangeQuality(int variable)
	{
		SettingsManager.instance.ChangeQuality(variable);
	}
}