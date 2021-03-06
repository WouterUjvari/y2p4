﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	private AudioPlayer aP;
	private Animator anim;
    public bool canPlay;
	public bool doorOpen;
	public float timeTillOpen;


	void Awake()
	{
		anim = GetComponent<Animator>();
        aP = GetComponent<AudioPlayer>();
		anim.SetBool("Open", doorOpen);
		timeTillOpen = aP.singleUse.length;
	}

	public void OpenCloseDoor()
	{
        if(canPlay)
        {
            if (aP.playedSingleUse == true)
            {
                timeTillOpen = 0;
            }
			canPlay = false;
            StartCoroutine(DoorTimer(timeTillOpen));
        }
	}

	public IEnumerator DoorTimer(float time)
	{
		yield return new WaitForSeconds(time);
		
        anim.SetTrigger("OpenClose");
	}
}
