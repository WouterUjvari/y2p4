using UnityEngine;
using TMPro;

public class KeyManager : MonoBehaviour 
{

    public static KeyManager instance;

    private AudioSource aS;
    [SerializeField]
    private AudioClip accesDenied;
    private int keysRequired;
    private int currentKeys;

    private bool canOpenLastDoor;

    public ObjectSnapper keySnapper;
    [SerializeField]
    private TextMeshProUGUI keyHolderDisplayText;
    [SerializeField]
    private Door doorToOpen;
    [SerializeField]
    private Collider elevatorTriggerCol;
    [SerializeField]
    private GameObject noTPZone;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        keysRequired = keySnapper.snapSpots.Count;
        aS = GetComponent<AudioSource>();
        elevatorTriggerCol.enabled = false;
    }

    private void Start()
    {
        canOpenLastDoor = false;
        keyHolderDisplayText.text = "ACCESS\nDENIED";
    }

    public void UseKey()
    {
        currentKeys++;

        if (currentKeys >= keysRequired)
        {
            AllKeysAquired();
        }
    }

    private void AllKeysAquired()
    {
        keyHolderDisplayText.text = "STATUS:\nONLINE";
        canOpenLastDoor = true;
    }

    public void OpenDoor()
    {
        if (!canOpenLastDoor)
        {
            aS.clip = accesDenied;
            aS.Play();
            return;
        }

        keyHolderDisplayText.text = "ACCESS\nGRANTED";
        doorToOpen.GetComponent<AudioPlayer>().PlayAudio();
        doorToOpen.OpenCloseDoor();
        noTPZone.SetActive(false);
        elevatorTriggerCol.enabled = true;
        TimeManager.instance.StopTimeForever();
    }
}
