using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRLMovementRestrictions : MonoBehaviour {

	public GameObject fadeImage;

	public void OnTriggerEnter(Collider other)
	{
		fadeImage.GetComponent<Animator>().SetTrigger("Fade");
		// gameObject.GetComponent<Rigidbody>().velocity = Vector3.back * Time.deltaTime;
	}

	public void OnTriggerExit(Collider other)
	{
		fadeImage.GetComponent<Animator>().SetTrigger("Fade");
	}
}
