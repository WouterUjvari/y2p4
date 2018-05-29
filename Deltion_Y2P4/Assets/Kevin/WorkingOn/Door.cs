using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	private Animator anim;
	public bool doorOpen;


	void Awake()
	{
		anim = GetComponent<Animator>();
		anim.SetBool("Open", doorOpen);
	}

	public void OpenCloseDoor()
	{
		anim.SetTrigger("OpenClose");
	}
}
