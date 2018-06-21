using UnityEngine;
using TMPro;

public class KeyManager : MonoBehaviour 
{

    private int keysRequired;
    private int currentKeys;

    private bool canOpenLastDoor;

    [SerializeField]
    private ObjectSnapper keySnapper;
    [SerializeField]
    private TextMeshProUGUI keyHolderDisplayText;
    [SerializeField]
    private Door doorToOpen;

    private void Awake()
    {
        keysRequired = keySnapper.snapSpots.Count;
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
            return;
        }

        keyHolderDisplayText.text = "ACCESS\nGRANTED";
        doorToOpen.OpenCloseDoor();
    }
}
