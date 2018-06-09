using System.Collections;
using UnityEngine;

public class PZ_RollTheBall : MonoBehaviour 
{

    [SerializeField]
    private Animator arenaAnim;
    [SerializeField]
    private Animator armAnim;

    [SerializeField]
    private GameObject ball;
    private GameObject activeBall;

    [SerializeField]
    private Transform ballStart;

    [SerializeField]
    private float ballTpInvinsibility = 1f;
    private bool ballCanTp = true;
    private float ballTpCooldown;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartPuzzle();
        }
        if (!ballCanTp)
        {
            if (ballTpCooldown > 0)
            {
                ballTpCooldown -= Time.deltaTime;
            }
            else
            {
                ballCanTp = true;
            }
        }
    }

    public void StartPuzzle()
    {
        StartCoroutine(OpenPuzzle());
    }

    private IEnumerator OpenPuzzle()
    {
        armAnim.SetTrigger("OpenClose");

        yield return new WaitForSeconds(1.5f);

        activeBall = Instantiate(ball, ballStart.position, Quaternion.identity, arenaAnim.transform);
    }

    private IEnumerator ClosePuzzle()
    {
        yield return new WaitForSeconds(2f);
        armAnim.SetTrigger("OpenClose");
    }

    public void RotateArenaRight()
    {
        arenaAnim.SetTrigger("RotateRight");
    }

    public void RotateArenaLeft()
    {
        arenaAnim.SetTrigger("RotateLeft");
    }

    public void TeleportBall(Transform destination)
    {
        if (ballCanTp)
        {
            activeBall.transform.position = destination.position;

            ballTpCooldown = ballTpInvinsibility;
            ballCanTp = false;
        }
    }

    public void CompletePuzzle()
    {
        Destroy(activeBall);
        StartCoroutine(ClosePuzzle());
    }
}
