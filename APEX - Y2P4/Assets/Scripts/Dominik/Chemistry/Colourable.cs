using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Colourable : MonoBehaviour 
{

    [SerializeField]
    private float colorLerpSpeed = 1f;
    [SerializeField]
    private UnityEvent onLerpColor;

    private bool isLerpingColor;
    private List<Material> myMaterials = new List<Material>();

    private void Awake()
    {
        Renderer[] myRenderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < myRenderers.Length; i++)
        {
            myMaterials.AddRange(myRenderers[i].materials);
        }
    }

    public void ReColor(Color newColor)
    {
        if (isLerpingColor)
        {
            return;
        }

        StartCoroutine(ChangeColor(newColor));
        onLerpColor.Invoke();
    }

    private IEnumerator ChangeColor(Color newColor)
    {
        isLerpingColor = true;
        float lerpTime = 0;

        for (int i = 0; i < myMaterials.Count; i++)
        {
            Color originalColor = myMaterials[i].color;

            if (myMaterials[i].HasProperty("_Color"))
            {
                while (myMaterials[i].color != newColor)
                {
                    myMaterials[i].color = Color.Lerp(originalColor, newColor, lerpTime);

                    lerpTime += Time.deltaTime * colorLerpSpeed;
                    yield return null;
                }
            }
        }

        isLerpingColor = false;
    }
}
