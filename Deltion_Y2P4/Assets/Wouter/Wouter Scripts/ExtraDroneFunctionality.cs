﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraDroneFunctionality : MonoBehaviour {

    public Animator anim;

    public List<GameObject> listOfItems = new List<GameObject>();
    public GameObject giftingItem;

    public Transform claw;

    public void SpawnInClaw()
    {
        if(giftingItem == null)
        {
            giftingItem = listOfItems[0];
        }


        GameObject g = Instantiate(giftingItem, claw.position, claw.rotation);
        //g.transform.parent = claw.transform;
        //g.GetComponent<Rigidbody>().isKinematic = true;

        FixedJoint joint = claw.gameObject.AddComponent<FixedJoint>();

        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;

        joint.connectedBody = g.GetComponent<Rigidbody>();
    }
        

}
