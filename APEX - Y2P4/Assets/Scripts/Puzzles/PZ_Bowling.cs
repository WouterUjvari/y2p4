using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PZ_Bowling : Puzzle 
{

    private bool isResettingPins;
    private int pinsHit;
    private Collider[] ballCheckCollidersInRange = new Collider[30];

    private int requiredPoints;
    private int currentPoints;

    [SerializeField]
    private Animator doorAnim;

    [Header("Gameplay")]

    [SerializeField]
    private GameObject pin;
    [SerializeField]
    private List<Transform> pinsSpawns = new List<Transform>();
    [SerializeField]
    private float collisionTimeBeforeResetting = 4f;
    [SerializeField]
    private Transform ballCheckArea;
    [SerializeField]
    private float ballCheckRadius = 2f;
    [SerializeField]
    private Transform ballSpawn;
    [SerializeField]
    private GameObject closePuzzleDetectionZone;

    [Header("Points")]

    [SerializeField]
    private int minReqPoints;
    [SerializeField]
    private int maxReqPoints;
    [SerializeField]
    private TextMeshProUGUI requiredPointsText;
    [SerializeField]
    private TextMeshProUGUI currentPointsText;
    [SerializeField]
    private Animator pointsScreenAnim;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ballCheckArea.position, ballCheckRadius);
    }

    private void Awake()
    {
        requiredPoints = Random.Range(minReqPoints, maxReqPoints);
        requiredPointsText.text = requiredPoints.ToString();
        currentPointsText.text = currentPoints.ToString();
    }

    public override void StartPuzzle()
    {
        FlowManager.instance.nextShipAIVoice(0);
        SpawnPins();
        ToggleDoor();
    }

    public void ToggleDoor()
    {
        doorAnim.SetTrigger("Toggle");
    }

    public void PinsHit()
    {
        if (!isResettingPins)
        {
            StartCoroutine(ResetPins());
        }
    }

    private IEnumerator ResetPins()
    {
        isResettingPins = true;

        yield return new WaitForSeconds(collisionTimeBeforeResetting);

        pinsHit = 0;
        for (int i = 0; i < pinsSpawns.Count; i++)
        {
            if (pinsSpawns[i].childCount > 0)
            {
                if (pinsSpawns[i].GetChild(0).transform.up.y < -0.05f ||
                    pinsSpawns[i].GetChild(0).transform.up.y > 0.05f)
                {
                    pinsHit++;
                }
            }
        }

        CheckForBallInArea();
        SpawnPins();
        CalculatePoints(pinsHit);

        isResettingPins = false;
    }

    private void SpawnPins()
    {
        for (int i = 0; i < pinsSpawns.Count; i++)
        {
            if (pinsSpawns[i].childCount > 0)
            {
                Destroy(pinsSpawns[i].GetChild(0).gameObject);
            }

            GameObject newPin = Instantiate(pin, pinsSpawns[i].position, pinsSpawns[i].rotation, pinsSpawns[i]);
            newPin.GetComponent<CollisionEventZone>().collisionEvent.AddListener(PinsHit);
        }
    }

    private void CheckForBallInArea()
    {
        int collidersFound = Physics.OverlapSphereNonAlloc(ballCheckArea.position, ballCheckRadius, ballCheckCollidersInRange);

        for (int i = 0; i < collidersFound; i++)
        {
            if (ballCheckCollidersInRange[i].transform.tag == "BowlingBall")
            {
                Rigidbody ballRb = ballCheckCollidersInRange[i].GetComponent<Rigidbody>();
                ballRb.velocity = Vector3.zero;
                ballCheckCollidersInRange[i].transform.position = ballSpawn.position;
                ballRb.AddForce(ballSpawn.forward, ForceMode.Impulse);
            }
        }
    }

    private void CalculatePoints(int amount)
    {
        pointsScreenAnim.SetTrigger("Trigger");

        currentPoints += amount;
        currentPointsText.text = currentPoints.ToString();

        if (currentPoints >= requiredPoints)
        {
            CompletePuzzle();
        }
    }

    public override void CompletePuzzle()
    {
        FlowManager.instance.nextShipAIVoice(0);
        FlowManager.instance.nextPuzzle(0);
        closePuzzleDetectionZone.SetActive(true);
    }
}
