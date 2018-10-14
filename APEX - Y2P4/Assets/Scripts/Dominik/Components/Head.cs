using UnityEngine;

public class Head : MonoBehaviour 
{

    private bool canPlayLiquidAnim = true;
    private Animator drinkLiquidAnim;

    private void Awake()
    {
        drinkLiquidAnim = GetComponent<Animator>();
    }

    public void StartDrinking()
    {
        if (!canPlayLiquidAnim)
        {
            return;
        }

        canPlayLiquidAnim = false;
        drinkLiquidAnim.SetTrigger("DrinkLiquid");
    }

    public void AnimationEventStopDrinking()
    {
        canPlayLiquidAnim = true;
    }
}
