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
        NextShipAIVoice(1);
	}

    private void Update()
    {
        DebugOverride();
    }

    public void NextPuzzle(float time)
	{
		if(currentPuzzle <= puzzles.Count - 1)
		{
            print("next puzzle");
			StartCoroutine(PauseBetweenPuzzles(time));
		}
	}

	public void NextAnouncerVoice(float time)
	{
		if(currentAudioAnouncer <= clipsForAnouncer.Count - 1)
		{
			anouncer.clips = clipsForAnouncer[currentAudioAnouncer].clips;
			StartCoroutine(NextAnouncerVoiceTimer(time));
		}
	}

	public void NextShipAIVoice(float time)
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
        print(currentPuzzle);
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


    public void DebugOverride()
    {
        if(Input.GetKey(KeyCode.U))
        {
            NextPuzzle(0);
        }
    }


    public void DroneBuildEvent()
    {
        NextAnouncerVoice(0);
        StartCoroutine(DroneBuildTimer());
    }

    public IEnumerator DroneBuildTimer()
    {
        yield return new WaitForSeconds(10);
        GameObject.FindObjectOfType<Drone>().GoLookAtPlayer();
        ExtraDroneFunctionality.instance.ToggleDroneCam(true);
        yield return new WaitForSeconds(12);
        GameObject.FindObjectOfType<Drone>().GetNewState();
        ExtraDroneFunctionality.instance.ToggleDroneCam(false);
        NextPuzzle(4);
    }
}