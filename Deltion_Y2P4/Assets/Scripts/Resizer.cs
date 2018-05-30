using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resizer : MonoBehaviour
{

    private bool canResize = true;
    private Animator anim;

    private List<Resizable> toResize = new List<Resizable>();
    private List<GameObject> toResizeObjectReferences = new List<GameObject>();

    private enum Setting
    {
        Normal,
        Shrink,
        Enlarge
    }
    [SerializeField]
    private Setting resizeSetting;

    [SerializeField]
    private TextMeshProUGUI resizeSettingText;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        resizeSettingText.text = resizeSetting.ToString();
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

    public void ChangeResizerSetting()
    {
        if (!canResize)
        {
            return;
        }

        int nextSetting = (int)resizeSetting + 1;

        if (nextSetting > 2)
        {
            nextSetting = 0;
        }

        resizeSetting = (Setting)nextSetting;
        resizeSettingText.text = resizeSetting.ToString();
    }

    public void AnimationEventResizeObjects()
    {
        switch (resizeSetting)
        {
            case Setting.Normal:

                for (int i = 0; i < toResize.Count; i++)
                {
                    toResize[i].StartResize(Resizable.ResizeSetting.Normal);
                }
                break;
            case Setting.Shrink:

                for (int i = 0; i < toResize.Count; i++)
                {
                    toResize[i].StartResize(Resizable.ResizeSetting.Shrunken);
                }
                break;
            case Setting.Enlarge:

                for (int i = 0; i < toResize.Count; i++)
                {
                    toResize[i].StartResize(Resizable.ResizeSetting.Enlarged);
                }
                break;
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
