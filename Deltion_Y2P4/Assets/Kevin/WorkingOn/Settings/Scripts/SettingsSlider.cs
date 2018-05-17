using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSlider : SettingsButton {

	public Vector3 sliderMin;
	public float maxMovement;
	private float resultMultiplier;

	void Start()
	{
		sliderMin = transform.position;
		resultMultiplier = -80 / maxMovement;
		print(resultMultiplier);
	}

	void Update()
	{
		MovingSlider();
	}

	public void MovingSlider()
	{
		float sliderValue;
		sliderValue = Vector3.Distance(sliderMin, transform.position);
		sliderValue *= resultMultiplier;
		print(sliderValue);
		ChangeVolume(sliderValue);
	}

	public void ChangeVolume(float variable)
	{
		SettingsManager.instance.GameVolume(variable);
	}
}
