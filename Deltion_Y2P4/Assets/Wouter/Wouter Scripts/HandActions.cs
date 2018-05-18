using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandActions : MonoBehaviour {

    public Animator anim;
    public float timeline;
    public bool press;

    void Update()
    {
		anim.SetFloat("TimeLine", Mathf.Clamp(timeline, 0, 0.9f));
        anim.SetBool("Press", press);

    }
}
