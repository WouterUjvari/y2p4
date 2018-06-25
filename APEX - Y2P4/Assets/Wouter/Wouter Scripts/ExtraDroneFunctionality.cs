using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraDroneFunctionality : MonoBehaviour
{

    public Animator anim;

    public List<GameObject> listOfItems = new List<GameObject>();
    public GameObject giftingItem;

    public Transform claw;

    [Header("Drone Player Cam")]
    [SerializeField] private GameObject faceText;
    [SerializeField] private GameObject droneCamImage;
    [SerializeField] private GameObject droneCam;

    private void Awake()
    {
        droneCam.SetActive(false);
    }

    private void Update()
    {
        if (droneCam.activeInHierarchy)
        {
            Vector3 target = VRPlayerMovementManager.instance.headTransform.position;
            target.y = droneCam.transform.position.y;

            droneCam.transform.LookAt(target);
        }
    }

    public void SpawnInClaw()
    {
        Destroy(claw.GetComponent<FixedJoint>());
        //claw.GetComponent<FixedJoint>().connectedBody = null;
        //Destroy(claw.GetComponent<FixedJoint>());
        if (giftingItem == null)
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
        g.GetComponent<Interactable>().onInteract.AddListener(DestroyJoints);
        //Destroy(g.GetComponent<Rigidbody>());
    }

    public void DestroyJoints()
    {
        //claw.GetComponent<FixedJoint>().connectedBody = null;
        Destroy(claw.GetComponent<FixedJoint>());
        anim.SetTrigger("Retract");
    }

    public void ToggleDroneCam(bool b)
    {
        faceText.SetActive(b ? false : true);
        droneCam.SetActive(b ? true : false);
        droneCamImage.SetActive(b ? true : false);
    }
}
