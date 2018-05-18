using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour 
{

    [SerializeField]
    private Material highlightMat;

    private MeshRenderer[] renderers;

	private bool isHighlighted;

    private void Awake()
    {
        if (highlightMat == null)
        {
            return;
        }

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
        if (highlightMat == null)
        {
            return;
        }

        highlightMat.SetFloat("Vector1_8DB509E0", 0);
		isHighlighted = true;
    }

    public void DeHighlight()
    {
        if (highlightMat == null)
        {
            return;
        }

        highlightMat.SetFloat("Vector1_8DB509E0", 1);
		isHighlighted = false;
    }
}
