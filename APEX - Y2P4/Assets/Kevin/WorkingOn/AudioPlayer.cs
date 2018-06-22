using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

	public AudioSource audioSource;
	public List<AudioClip> clips = new List<AudioClip>();
	private bool nextClip;
	public bool playAllClips;
	private int currentClip;
	public bool playedSound;
	public AudioClip singleUse;
	private bool playedSingleUse;

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
		audioSource.clip = clips[currentClip];
	}

	public void playAudio()
	{
		if(singleUse != null)
		{
			if(playedSingleUse == false)
			{
				StartCoroutine(SingleUse());
			}
			else
			{
				PlayMainAudio();
			}
		}
	}

	public void PlayMainAudio()
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

	public IEnumerator SingleUse()
	{
		print("in co routine");
		playedSingleUse = true;
		audioSource.clip = singleUse;
		audioSource.Play();
		yield return new WaitForSeconds(singleUse.length);
		PlayMainAudio();
	} 
}
