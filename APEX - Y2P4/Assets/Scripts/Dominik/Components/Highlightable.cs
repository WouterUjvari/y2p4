﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour 
{

    [SerializeField]
    private Material highlightMat;

    [SerializeField]
    private List<MeshRenderer> renderersToExclude = new List<MeshRenderer>();

    private MeshRenderer[] renderers;

	public bool isHighlighted { private set; get; }

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
            if (!renderersToExclude.Contains(renderers[i]))
            {
                List<Material> materials = new List<Material>();
                materials.AddRange(renderers[i].materials);

                materials.Add(highlightMat);

                renderers[i].materials = materials.ToArray();
            }
        }
    }

    public void Highlight()
    {
        if (highlightMat == null || isHighlighted)
        {
            return;
        }

        highlightMat.SetFloat("_Alpha", 0.5f);
		isHighlighted = true;
    }

    public void DeHighlight()
    {
        if (highlightMat == null || !isHighlighted)
        {
            return;
        }

        highlightMat.SetFloat("_Alpha", 0);
		isHighlighted = false;
    }
}
