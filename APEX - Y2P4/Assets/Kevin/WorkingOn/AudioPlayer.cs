using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

	public AudioSource audioSource;
	public List<AudioClip> clips = new List<AudioClip>();

	private bool nextClip;
	public bool playAllClips;
	private int currentClip;
	public AudioClip playerName;

	void Awake()
	{
		UpdateAudioClip();
	}

	void Update()
	{
		if(playAllClips)
		{
			if(nextClip)
			{
				if(!audioSource.isPlaying)
				{
					playAudio();
				}
			}
		}
	}

	private void UpdateAudioClip()
	{
		print("Update");
		audioSource.clip = clips[currentClip];
	}

	public void playAudio()
	{
		UpdateAudioClip();
		audioSource.Play();

		if(clips.Count > 1)
		{
			
			if(clips.Count - 1 != currentClip)
			{
				nextClip = true;
				currentClip += 1;
			}
			else
			{
				nextClip = false;
				print("reset");
				currentClip = 0;
			}
		}
	}
}
