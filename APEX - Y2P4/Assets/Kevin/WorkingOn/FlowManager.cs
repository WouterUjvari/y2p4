using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour {
	public static FlowManager instance;

	public AudioPlayer shipAI;
	public AudioPlayer anouncer;
	private int currentAudioAnouncer;
	private int currentAudioAI;
	public List<ClipList> clipsForShipAI = new List<ClipList>();
	public List<ClipList> clipsForAnouncer = new List<ClipList>();

	public int currentPuzzle;
	public List<Puzzle> puzzles = new List<Puzzle>();

	void Start () 
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

	public void nextPuzzle(float time)
	{
		if(currentPuzzle <= puzzles.Count - 1)
		{
			StartCoroutine(PauseBetweenPuzzles(time));
		}
	}

	public void nextAnouncerVoice(float time)
	{
		if(currentAudioAnouncer <= clipsForAnouncer.Count - 1)
		{
			anouncer.clips = clipsForAnouncer[currentAudioAnouncer].clips;
			StartCoroutine(NextAnouncerVoiceTimer(time));
		}
	}

	public void nextShipAIVoice(float time)
	{
		if(currentAudioAI <= clipsForShipAI.Count - 1)
		{
			shipAI.clips = clipsForShipAI[currentAudioAI].clips;
			StartCoroutine(NextShipAIVoiceTimer(time));
		}
	}

	public IEnumerator PauseBetweenPuzzles(float time)
	{
		yield return new WaitForSeconds(time);
		puzzles[currentPuzzle].StartPuzzle();
		currentPuzzle += 1;
	}

	public IEnumerator NextShipAIVoiceTimer(float time)
	{
		yield return new WaitForSeconds(time);
		shipAI.PlayMainAudio();
		currentAudioAI += 1;
	}

	public IEnumerator NextAnouncerVoiceTimer(float time)
	{
		yield return new WaitForSeconds(time);
		anouncer.PlayMainAudio();
		currentAudioAnouncer += 1;
	}

}