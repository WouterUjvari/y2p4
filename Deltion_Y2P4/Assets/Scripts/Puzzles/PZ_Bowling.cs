using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PZ_Bowling : MonoBehaviour 
{

    public bool canSpawn;

    [SerializeField]
    private float spawnTimer;
    private float spawnCooldown;
    [SerializeField]
    private float pinActiveTime;

    private List<PZ_BowlingPin> pins = new List<PZ_BowlingPin>();

    [SerializeField]
    private GameObject pin;

    [SerializeField]
    private List<Transform> pinSpawns = new List<Transform>();

    private void Awake()
    {
        PlacePins();
    }

    private void Update()
    {
        if (!canSpawn)
        {
            return;
        }

        if (spawnCooldown > 0)
        {
            spawnCooldown -= Time.deltaTime;
        }
        else
        {
            spawnCooldown = Random.Range((float)(0.75 * spawnTimer), (float)(1.25 * spawnTimer));
            SpawnPin();
        }
    }

    private void PlacePins()
    {
        if (pins.Count > 0)
        {
            for (int i = 0; i < pins.Count; i++)
            {
                Destroy(pins[i].gameObject);
            }

            pins.Clear();
        }

        for (int i = 0; i < pinSpawns.Count; i++)
        {
            GameObject newPin = Instantiate(pin, pinSpawns[i].position, Quaternion.identity);
            PZ_BowlingPin newPinComponent = newPin.GetComponent<PZ_BowlingPin>();
            newPinComponent.activeTime = pinActiveTime;
            pins.Add(newPinComponent);

            CollisionEventZone colEZ = newPin.GetComponent<CollisionEventZone>();
            colEZ.collisionEvent.AddListener(BallHitPin);
        }
    }

    private void SpawnPin()
    {
        PZ_BowlingPin inActivePin = null;
        while (inActivePin == null)
        {
            int i = Random.Range(0, pins.Count);
            if (!pins[i].isActive)
            {
                inActivePin = pins[i];
            }
        }

        inActivePin.Activate();
    }

    public void BallHitPin()
    {
        canSpawn = false;
    }
}
