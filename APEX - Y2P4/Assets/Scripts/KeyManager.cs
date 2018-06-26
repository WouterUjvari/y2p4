using UnityEngine;
using TMPro;

public class KeyManager : MonoBehaviour 
{

    private AudioSource aS;
    [SerializeField]
    private AudioClip accesDenied;
    private int keysRequired;
    private int currentKeys;

    private bool canOpenLastDoor;

    [SerializeField]
    private ObjectSnapper keySnapper;
    [SerializeField]
    private TextMeshProUGUI keyHolderDisplayText;
    [SerializeField]
    private Door doorToOpen;
    [SerializeField]
    private GameObject noTPZone;

    private void Awake()
    {
        keysRequired = keySnapper.snapSpots.Count;
        aS = GetComponent<AudioSource>();
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
        doorToOpen.GetComponent<AudioPlayer>().playAudio();
        doorToOpen.OpenCloseDoor();
        noTPZone.SetActive(false);
        TimeManager.instance.StopTimeForever();
    }
}
