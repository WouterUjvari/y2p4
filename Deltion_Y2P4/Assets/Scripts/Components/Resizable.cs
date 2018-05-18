using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizable : MonoBehaviour 
{

    [SerializeField]
    private float resizeSpeed = 1;

    [SerializeField]
    private float smallSize = 0.3f;

    public void StartResize()
    {
        StartCoroutine(Resize((transform.localScale.x < 1) ? true : false));
        print("t");
    }

    private IEnumerator Resize(bool up)
    {
        float newResizeSpeed = Random.Range(0.75f * resizeSpeed, 1.25f * resizeSpeed);

        float targetSize = up ? 1 : smallSize;
        float resizeStep = Time.deltaTime * newResizeSpeed;

        if (up)
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
    }
}
