using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour {

	public Animator fadeInOut;
	// Use this for initialization
	void Start () 
	{
		//toggleFade();	
	}
	
	public void toggleFade()
	{
		fadeInOut.SetTrigger("Fade");
	}
}
