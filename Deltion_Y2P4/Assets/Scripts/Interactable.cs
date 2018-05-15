using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour 
{

    [SerializeField]
    private Material highlightMat;

    private MeshRenderer[] renderers;

    private void Awake()
    {
        highlightMat = Instantiate(highlightMat);

        renderers = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            List<Material> materials = new List<Material>();
            materials.AddRange(renderers[i].materials);

            materials.Add(highlightMat);

            renderers[i].materials = materials.ToArray();
        }
    }

    public void Highlight()
    {
        highlightMat.SetFloat("Vector1_8DB509E0", 0);
    }

    public void DeHighlight()
    {
        highlightMat.SetFloat("Vector1_8DB509E0", 1);
    }
}
