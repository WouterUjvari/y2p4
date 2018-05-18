using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSlider : SettingsButton {

	public enum VolumeType
	{
		Master,
		Music,
		Effects
	}

	public VolumeType volumeType;
	public Vector3 sliderMin;
	// the amount of distance it can move from start to end
	public float maxMovement;
	private float resultMultiplier;

	void Start()
	{
		sliderMin = transform.position;
		resultMultiplier = -80 / maxMovement;
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
		ChangeVolume(sliderValue);
	}

	public void ChangeVolume(float variable)
	{
		if(volumeType == VolumeType.Master)
		{
			SettingsManager.instance.GameVolume(variable);
		}
		
		if(volumeType == VolumeType.Music)
		{
			SettingsManager.instance.GameVolumeMusic(variable);
		}

		if(volumeType == VolumeType.Effects)
		{
			SettingsManager.instance.GameVolumeEffects(variable);
		}
	}
}
