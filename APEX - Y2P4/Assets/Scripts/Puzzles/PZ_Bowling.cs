using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PZ_Bowling : MonoBehaviour 
{

    private bool isResettingPins;
    private int pinsHit;
    private Collider[] ballCheckCollidersInRange = new Collider[15];

    [SerializeField]
    private Animator doorAnim;
    [SerializeField]
    private float collisionTimeBeforeResetting = 4f;
    [SerializeField]
    private GameObject pin;
    [SerializeField]
    private List<Transform> pinsSpawns = new List<Transform>();
    private List<Rigidbody> pinRbs = new List<Rigidbody>();
    [SerializeField]
    private Transform ballCheckArea;
    [SerializeField]
    private float ballCheckRadius = 2f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ballCheckArea.position, ballCheckRadius);
    }

    private void Awake()
    {
        SpawnPins();
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            CheckForBallInArea();
        }
    }
    public void ToggleDoor()
    {
        doorAnim.SetTrigger("Trigger");
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
        for (int i = 0; i < pinRbs.Count; i++)
        {
            if (pinRbs[i].velocity != Vector3.zero)
            {
                pinsHit++;
            }
        }

        CheckForBallInArea();
        SpawnPins();

        isResettingPins = false;
    }

    private void SpawnPins()
    {
        pinRbs.Clear();

        for (int i = 0; i < pinsSpawns.Count; i++)
        {
            if (pinsSpawns[i].childCount > 0)
            {
                Destroy(pinsSpawns[i].GetChild(0).gameObject);
            }

            GameObject newPin = Instantiate(pin, pinsSpawns[i].position, pinsSpawns[i].rotation, pinsSpawns[i]);

            Rigidbody rb = newPin.GetComponent<Rigidbody>();
            pinRbs.Add(rb);

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
                Destroy(ballCheckCollidersInRange[i].gameObject);
            }
        }
    }
}
