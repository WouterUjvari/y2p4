using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipUX : MonoBehaviour {

    public LineRenderer myLine;
    public TipUXManager tipUXmanager;
    public enum TipUXType {PressTarget, PickUpTarget, PressInteractKey, PressNavigationKey };
    public TipUXType tipuxtype;
    private Animator anim;

    [Header("Line transforms")]
    public Transform origin;
    public Transform hook;
    public Transform target;
    public string targetToFind;

    [Header("Rotate")]
    public Transform targetCamPos;
    public Transform targetCamRot;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public void Start()
    {
        anim = GetComponent<Animator>();
        if (targetToFind != "")
        {
            target = GameObject.Find(targetToFind).transform;
        }     
    }

    public void FixedUpdate()
    {
        myLine.SetPosition(0, origin.position);
        myLine.SetPosition(1, hook.position);
        myLine.SetPosition(2, target.position);

          
        
        if (tipuxtype == TipUXType.PickUpTarget)
        {

        }
        else if (tipuxtype == TipUXType.PressInteractKey)
        {
            if (VRInteractor.triggered)
            {
                QueNextTip();
            }
        }
        else if (tipuxtype == TipUXType.PressNavigationKey)
        {
           
        }
        
    }

    public void LateUpdate()
    {
        Vector3 desiredPosition = targetCamPos.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(targetCamRot);
    }

    public void QueNextTip()
    {
        anim.SetTrigger("Die");
    }

    public void KillTip()
    {
        tipUXmanager.tipIndex++;
        tipUXmanager.SpawnNewTip();
        //Destroy(this.gameObject);
    }
    public void KillhardCoded()
    {
        
        tipUXmanager.SpawnNewTip();
        anim.SetTrigger("Die");
    }




}
