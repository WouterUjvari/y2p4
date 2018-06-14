using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {


    public float ygravity;
    public float ydefault;
    public Animator anim;

    public bool dograv = true;
    

    

    void Start()
    {
        ygravity = Physics.gravity.y;
        dograv = true;
    }

    void Update()
    {
        anim.SetBool("On", dograv);

        Physics.gravity = new Vector3(0, ygravity, 0);
    }

    public void Switchgrav()
    {
        dograv = !dograv;
    }
}
