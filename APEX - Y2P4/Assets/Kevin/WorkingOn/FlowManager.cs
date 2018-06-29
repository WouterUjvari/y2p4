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
    public Drone drone;
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
        drone = FindObjectOfType<Drone>();
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
        FindObjectOfType<Drone>().GoLookAtPlayer();
        ExtraDroneFunctionality.instance.ToggleDroneCam(true);
        ExtraDroneFunctionality.instance.anim.SetTrigger("Point");
        yield return new WaitForSeconds(12);
        drone.GetNewState();
        ExtraDroneFunctionality.instance.ToggleDroneCam(false);
        NextPuzzle(4);
    }

    public void StartNda()
    {
        ExtraDroneFunctionality.instance.ToggleDroneCam(false);
        NextAnouncerVoice(0);
        ExtraDroneFunctionality.instance.itemIndex = 1;
        ExtraDroneFunctionality.instance.anim.SetTrigger("GiveItem");
        drone.GoLookAtPlayer();
    }
    public void CompleteNDA()
    {
        drone.GetNewState();
        ExtraDroneFunctionality.instance.anim.SetTrigger("Retract");
        Destroy(ExtraDroneFunctionality.instance.itemInHand, 0.1f);
        ExtraDroneFunctionality.instance.giftingItem = null;
        ExtraDroneFunctionality.instance.anim.SetTrigger("Salute");
        NextAnouncerVoice(0);
        ExtraDroneFunctionality.instance.itemIndex = 0;
        ExtraDroneFunctionality.instance.triggerName = "GiveItem";
        ExtraDroneFunctionality.instance.Invoke("TriggerAnimation", 2);
        NextPuzzle(10);
    }


    public void StartBall()
    {
        NextShipAIVoice(0);
        drone.Invoke("GoLookAtPlayer",3);
        NextAnouncerVoice(3);
        drone.Invoke("GetNewState",4);
    }
    public void CompleteBall()
    {
        NextShipAIVoice(0);
        NextAnouncerVoice(3);
        NextPuzzle(10);
    }


    public void StartPlanets()
    {
        NextShipAIVoice(0);
        NextAnouncerVoice(4);
    }
    public void CompletePlanets()
    {
        NextShipAIVoice(0);
        NextAnouncerVoice(4);
        ExtraDroneFunctionality.instance.itemIndex = 0;
        ExtraDroneFunctionality.instance.triggerName = "GiveItem";
        ExtraDroneFunctionality.instance.Invoke("TriggerAnimation", 4);
        NextPuzzle(12);
    }


    public void StartBowling()
    {
        NextShipAIVoice(0);
        NextAnouncerVoice(4);
    }
    public void CompleteBowling()
    {
        NextShipAIVoice(0);
        NextAnouncerVoice(2);
        NextPuzzle(12);
    }


    public void StartChemical()
    {
        NextShipAIVoice(0);
        NextAnouncerVoice(3);
    }
    public void CompleteChemical()
    {
        NextShipAIVoice(0);
        NextAnouncerVoice(4);
        ExtraDroneFunctionality.instance.itemIndex = 0;
        ExtraDroneFunctionality.instance.triggerName = "GiveItem";
        ExtraDroneFunctionality.instance.Invoke("TriggerAnimation", 10);
        NextPuzzle(18);
    }


    public void StartLight()
    {
        NextShipAIVoice(0);
        NextAnouncerVoice(4);
    }
    public void CompleteLight()
    {
        NextShipAIVoice(0);
        NextAnouncerVoice(4);
        NextPuzzle(15);
    }
}