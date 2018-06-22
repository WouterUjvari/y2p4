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

	private int currentPuzzle;
	public List<Puzzle> puzzles = new List<Puzzle>();
	public int timeBetweenPuzzles;

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

	public void nextPuzzle()
	{
		StartCoroutine(PauseBetweenPuzzles(timeBetweenPuzzles));
	}

	public void nextAnouncerVoice()
	{
		anouncer.clips = clipsForAnouncer[currentAudioAnouncer].clips;
		anouncer.PlayMainAudio();
		currentAudioAnouncer += 1;
	}

	public void nextShipAIVoice()
	{
		shipAI.clips = clipsForShipAI[currentAudioAI].clips;
		shipAI.PlayMainAudio();
		currentAudioAI += 1;
	}

	public IEnumerator PauseBetweenPuzzles(float time)
	{
		yield return new WaitForSeconds(time);
		puzzles[currentPuzzle].StartPuzzle();
	}
}