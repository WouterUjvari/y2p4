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

	public bool touched;

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

	public void Touching(bool b)
	{
		touched = b;
		if(b == false)
		{
			ChangeQuality();
		}
	} 

	public void MovingHandel()
	{
		if(touched)
		{
			float handelValue;
			handelValue = Vector3.Distance(handelMin, transform.position);
			qualitySlider.value = maxMovement - handelValue;
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
		}
	}

	public void ChangeQuality()
	{
		SettingsManager.instance.ChangeQuality(qualityIndex);
	}
}