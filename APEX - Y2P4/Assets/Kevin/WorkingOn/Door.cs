using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private AudioSource audio;
	private Animator anim;
    public bool canPlay;
	public bool doorOpen;


	void Awake()
	{
		anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
		anim.SetBool("Open", doorOpen);
	}

	public void OpenCloseDoor()
	{
        if(canPlay)
        {
            canPlay = false;
            anim.SetTrigger("OpenClose");
            audio.Play();
        }
	}
}
