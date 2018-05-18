using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PZ_Bowling : MonoBehaviour 
{

    public bool canSpawn;
    private bool isSpawningPin;

    [SerializeField]
    private float spawnTimer;

    private List<PZ_BowlingPin> pins = new List<PZ_BowlingPin>();

    [SerializeField]
    private GameObject pin;

    [SerializeField]
    private List<Transform> pinSpawns = new List<Transform>();

    private void Awake()
    {
        for (int i = 0; i < pinSpawns.Count; i++)
        {
            GameObject newPin = Instantiate(pin, pinSpawns[i].position, Quaternion.identity);
            pins.Add(newPin.GetComponent<PZ_BowlingPin>());
        }
    }

    private void Update()
    {
        if (!canSpawn)
        {
            return;
        }

        if (!isSpawningPin)
        {
            StartCoroutine(SpawnPin());
        }
    }

    private IEnumerator SpawnPin()
    {
        isSpawningPin = true;

        for (int i = 0; i < pins.Count; i++)
        {
            if (pins[i].isActive)
            {
                pins[i].DeActivate();
            }
        }

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

        yield return new WaitForSeconds(spawnTimer);

        isSpawningPin = false;
    }
}
