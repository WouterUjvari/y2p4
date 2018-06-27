using UnityEngine;

public class PZ_NDA : Puzzle
{

    private bool isSigning;

    public override void StartPuzzle()
    {
        // Drone holds out NDA.
    }

    public override void CompletePuzzle()
    {
        // Next puzzle.
    }

    public void IsSigning(bool b)
    {
        isSigning = b ? true : false;

        if (isSigning && !b)
        {
            CompletePuzzle();
        }
    }
}
