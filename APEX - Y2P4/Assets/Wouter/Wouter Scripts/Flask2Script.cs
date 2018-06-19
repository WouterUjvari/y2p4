using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask2Script : MonoBehaviour {


    public Animator anim;
    public float liquidAmount;

    void Update()
    {
        anim.SetFloat("Timeline", Mathf.Clamp(liquidAmount, 0, 0.999f));
    }


}
