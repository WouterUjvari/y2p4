using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsQuality : MonoBehaviour {


	private Vector3 handelMin;

	private float resultStep;

	public float maxMovement;

	private float lastStep;

	public int qualityIndex = 5;

	void Start()
	{
		//handelMin = new Vector3(WrapAngle(transform.rotation.eulerAngles.x), WrapAngle(transform.rotation.eulerAngles.y), WrapAngle(transform.rotation.eulerAngles.z));
		handelMin = transform.position;
		resultStep = maxMovement / 5;
		//resultStep = Mathf.Round(resultStep * 100f) / 100f;
	}

	void Update()
	{
		MovingHandel();
	}

	public void MovingHandel()
	{
		float handelValue;
		//handelValue = Vector3.Distance(handelMin, new Vector3(WrapAngle(transform.rotation.eulerAngles.x), WrapAngle(transform.rotation.eulerAngles.y), WrapAngle(transform.rotation.eulerAngles.z)));
		handelValue = Vector3.Distance(handelMin, transform.position);
		print(handelValue);
		//print(WrapAngle(transform.rotation.eulerAngles.x));
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