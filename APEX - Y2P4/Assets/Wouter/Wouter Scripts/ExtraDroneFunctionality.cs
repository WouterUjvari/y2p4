using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraDroneFunctionality : MonoBehaviour
{
    public static ExtraDroneFunctionality instance;

    public Animator anim;

    public List<GameObject> listOfItems = new List<GameObject>();
    public GameObject giftingItem;

    public Transform claw;
    public GameObject itemInHand;
    public int itemIndex;
    [HideInInspector]
    public string triggerName;

    [Header("Drone Player Cam")]
    [SerializeField] private GameObject faceText;
    [SerializeField] private GameObject droneCamImage;
    [SerializeField] private GameObject droneCam;

    private void Awake()
    {
        instance = this;
        droneCam.SetActive(false);
    }

    private void Update()
    {
        if (droneCam.activeInHierarchy)
        {
            Vector3 target = VRPlayerMovementManager.instance.headTransform.position;
            //target.y = droneCam.transform.position.y;

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
            giftingItem = listOfItems[itemIndex];
        }

        itemInHand = Instantiate(giftingItem, claw.position, claw.rotation);

        //g.transform.parent = claw.transform;
        //g.GetComponent<Rigidbody>().isKinematic = true;

        FixedJoint joint = claw.gameObject.AddComponent<FixedJoint>();

        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;

        joint.connectedBody = itemInHand.GetComponent<Rigidbody>();

        Interactable interactable = itemInHand.GetComponent<Interactable>();
        if (interactable)
        {
            itemInHand.GetComponent<Interactable>().onInteract.AddListener(DestroyJoints);
        }

        if (giftingItem.tag == "Key")
        {
            interactable.onDeInteract.AddListener(() => KeyManager.instance.keySnapper.SnapObject(itemInHand.transform));
        }
        //Destroy(g.GetComponent<Rigidbody>());
    }

    public void TriggerAnimation()
    {
        anim.SetTrigger(triggerName);
    }

    public void DestroyJoints()
    {
        //claw.GetComponent<FixedJoint>().connectedBody = null;
        giftingItem = null;
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
