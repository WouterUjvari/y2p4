using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecorderScript : MonoBehaviour {


    public AudioSource playerNameAudioSource;

    void Start()
    {
        //playerNameAudioSource.clip = Microphone.Start("Microphone", false, 5, 44100);

        playerNameAudioSource.clip = Microphone.Start("Microphone", true, 5, 44100);
    }
}
