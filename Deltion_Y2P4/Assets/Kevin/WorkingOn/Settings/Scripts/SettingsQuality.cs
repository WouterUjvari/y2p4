using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsQuality : MonoBehaviour {


	private Vector3 handelMin;

	private float resultStep;

	public float maxMovement;

	private float lastStep;

	private int qualityIndex = 5;

	void Start()
	{
		handelMin = new Vector3(WrapAngle(transform.rotation.eulerAngles.x), WrapAngle(transform.rotation.eulerAngles.y), WrapAngle(transform.rotation.eulerAngles.z));
		resultStep = maxMovement / 5;
		resultStep = Mathf.Round(resultStep * 100f) / 100f;
	}

	void Update()
	{
		MovingHandel();
		print(qualityIndex);
	}

	public void MovingHandel()
	{
		float handelValue;
		handelValue = Vector3.Distance(handelMin, new Vector3(WrapAngle(transform.rotation.eulerAngles.x), WrapAngle(transform.rotation.eulerAngles.y), WrapAngle(transform.rotation.eulerAngles.z)));
		print(handelValue);
		print(WrapAngle(transform.rotation.eulerAngles.x));
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
		if(handelValue <= 2 && qualityIndex != 5)
		{
			qualityIndex += 1;
			lastStep -= resultStep;
		}
		ChangeQuality(qualityIndex);
	}

	public float WrapAngle(float angle)
	{
		angle%=360;
		if(angle >180)
		{
			return angle - 360;
		}	

		return angle;
	}

	public void ChangeQuality(int variable)
	{
		SettingsManager.instance.ChangeQuality(variable);
	}
}