using UnityEngine;

public class ReleaseObject : MonoBehaviour 
{

    [SerializeField]
    private Rigidbody toRelease;

    public void Release()
    {
        toRelease.isKinematic = false;
    }
}
