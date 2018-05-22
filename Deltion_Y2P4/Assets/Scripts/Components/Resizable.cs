using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizable : MonoBehaviour 
{

    [SerializeField]
    private float resizeSpeed = 1;

    [SerializeField]
    private float smallSize = 0.3f;

    [SerializeField]
    private float bigSize = 1.5f;

    public enum ResizeSetting
    {
        Normal,
        Shrunken,
        Enlarged
    }
    [HideInInspector]
    public ResizeSetting resizeSetting;

    public void StartResize(ResizeSetting resizeTo)
    {
        StartCoroutine(Resize(resizeTo));
    }

    private IEnumerator Resize(ResizeSetting resizeTo)
    {
        float newResizeSpeed = Random.Range(0.75f * resizeSpeed, 1.25f * resizeSpeed);
        float resizeStep = Time.deltaTime * newResizeSpeed;

        float targetSize = 1;
        switch (resizeTo)
        {
            case ResizeSetting.Normal:

                targetSize = 1;
                break;
            case ResizeSetting.Shrunken:

                targetSize = smallSize;
                break;
            case ResizeSetting.Enlarged:

                targetSize = bigSize;
                break;
        }

        if (transform.localScale.x < targetSize)
        {
            while (transform.localScale.x < targetSize)
            {
                transform.localScale += new Vector3(resizeStep, resizeStep, resizeStep);
                yield return null;
            }
        }
        else
        {
            while (targetSize < transform.localScale.x)
            {
                transform.localScale -= new Vector3(resizeStep, resizeStep, resizeStep);
                yield return null;
            }
        }

        transform.localScale = Vector3.one * targetSize;
    }
}
