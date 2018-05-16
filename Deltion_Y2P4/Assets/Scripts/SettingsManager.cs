using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SettingsManager : MonoBehaviour {

	public static SettingsManager instance;
	public AudioMixer mainMixer;

	public bool screenMode;

	void Start()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
	}

	public void GameVolume(float volume)
	{
		mainMixer.SetFloat("Master",volume);
	}

	public void GameVolumeMusic(float volume)
	{
		mainMixer.SetFloat("Music",volume);
	}

	public void GameVolumeEffects(float volume)
	{
		mainMixer.SetFloat("SoundEffects",volume);
	}

	public void ChangeQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void ChangeScreenMode(int screenModeIndex)
	{	
		if(screenModeIndex == 0)
		{
			screenMode = true;
			Screen.fullScreen = true;
		}
		else if(screenModeIndex == 1)
		{
			screenMode = false;
			Screen.fullScreen = false;
		}
	}
}
