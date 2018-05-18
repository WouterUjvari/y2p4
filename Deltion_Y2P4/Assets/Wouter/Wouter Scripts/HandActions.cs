using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandActions : MonoBehaviour {

    public Animator anim;
    public float timeline;
    public bool press;

    void Update()
    {
        anim.SetFloat("TimeLine", timeline);
        anim.SetBool("Press", press);

    }
}
