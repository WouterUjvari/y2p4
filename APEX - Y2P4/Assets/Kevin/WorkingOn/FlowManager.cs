using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour {

	public delegate void PuzzleEvents();

	private FlowManager instance;
	private PuzzleEvents puzzleEvent;

	public AudioPlayer shipAI;
	public AudioPlayer anouncer;
	public int currentAudioAnouncer;
	public int currentAudioAI;

	public List<ClipList> clipsForShipAI = new List<ClipList>();
	public List<ClipList> clipsForAnouncer = new List<ClipList>();
	
	private int currentEvent;
	public int eventAmount;

	public List<PuzzleEvents> PuzzleEventList = new List<PuzzleEvents>();
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

		for (int i = 0; i < eventAmount; i++)
		{
			PuzzleEventList.Add(puzzleEvent);
		}
	}

	public void nextEvent()
	{
		PuzzleEventList[currentEvent]();
		eventAmount ++;
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
}