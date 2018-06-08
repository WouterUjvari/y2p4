using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour {

	public delegate void PuzzleEvent();

	public GameFlowManager instance;
	public int currentEvent;
	public AudioPlayer anounncer;
	public List<PuzzleEvent> eventList = new List<PuzzleEvent>(); 
	public List<ClipList> scriptableList = new List<ClipList>();


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

	public void TriggerNextEvent()
	{
		anounncer.clips = scriptableList[currentEvent].clips;
		eventList[currentEvent]();
	}
}
