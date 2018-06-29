using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

	public AudioSource audioSource;
	public List<AudioClip> clips = new List<AudioClip>();
	private bool nextClip;
	public bool playAllClips;
	public int currentClip;
	public bool playedSound;
	public AudioClip singleUse;
    [HideInInspector]
	public bool playedSingleUse;

	void Update()
	{
		if(playAllClips)
		{
			if(nextClip)
			{
				if(!audioSource.isPlaying)
				{
					PlayAudio();
				}
			}
		}
	}

	private void UpdateAudioClip()
	{
		audioSource.clip = clips[currentClip];
	}

	public void PlayAudio()
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
		else
		{
			PlayMainAudio();
		}
	}

	public void PlayMainAudio()
	{
		UpdateAudioClip();
		audioSource.Play();
		if(clips.Count > 1)
		{
			if(currentClip < clips.Count - 1 )
			{
                nextClip = true;
				currentClip += 1;
			}
			else if(clips.Count - 1 >= currentClip)
			{
				nextClip = false;
				currentClip = 0;
			}
		}
	}

	public IEnumerator SingleUse()
	{
		playedSingleUse = true;
		audioSource.clip = singleUse;
		audioSource.Play();
		yield return new WaitForSeconds(singleUse.length);
		PlayMainAudio();
	} 
}
