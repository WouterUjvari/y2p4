using UnityEngine;

public class PZ_RollTheBall : MonoBehaviour 
{

    [SerializeField]
    private Animator arenaAnim;

    public void RotateArenaRight()
    {
        arenaAnim.SetTrigger("RotateRight");
    }

    public void RotateArenaLeft()
    {
        arenaAnim.SetTrigger("RotateLeft");
    }
}
