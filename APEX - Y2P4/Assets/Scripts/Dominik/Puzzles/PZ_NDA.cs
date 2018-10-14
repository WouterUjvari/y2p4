using UnityEngine;

public class PZ_NDA : Puzzle
{

    private bool signedNDA;

    private bool isSigning;

    public override void StartPuzzle()
    {
        FlowManager.instance.StartNda();
    }

    public override void CompletePuzzle()
    {
        FlowManager.instance.CompleteNDA();
    }

    public void IsSigning(bool b)
    {
        if (isSigning && !b)
        {
            if (!signedNDA)
            {
                Invoke("CompletePuzzle",0.5f);
                signedNDA = true;
            }
        }

        isSigning = b ? true : false;
    }
}
