using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizer : MonoBehaviour 
{

    private bool canResize = true;
    private Animator anim;

    private List<Resizable> toResize = new List<Resizable>();
    private List<GameObject> toResizeObjectReferences = new List<GameObject>();

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void StartResize()
    {
        if (!canResize)
        {
            return;
        }

        canResize = false;
        anim.SetTrigger("Resize");
    }

    public void AnimationEventResizeObjects()
    {
        for (int i = 0; i < toResize.Count; i++)
        {
            toResize[i].StartResize();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Resizable resizable = other.GetComponent<Resizable>();
        if (resizable != null)
        {
            if (!toResize.Contains(resizable))
            {
                toResize.Add(resizable);
                toResizeObjectReferences.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (toResizeObjectReferences.Contains(other.gameObject))
        {
            toResize.Remove(other.GetComponent<Resizable>());
            toResizeObjectReferences.Remove(other.gameObject);
        }
    }

    public void ResetCanResize()
    {
        canResize = true;
    }
}
