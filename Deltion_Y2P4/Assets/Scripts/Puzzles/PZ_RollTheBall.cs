using UnityEngine;

public class PZ_RollTheBall : MonoBehaviour 
{

    [SerializeField]
    private Animator arenaAnim;

    [SerializeField]
    private GameObject ball;
    private GameObject activeBall;

    [SerializeField]
    private Transform ballStart;

    [SerializeField]
    private float ballTpInvinsibility = 1f;
    private bool ballCanTp = true;
    private float ballTpCooldown;

    private void Awake()
    {
        activeBall = Instantiate(ball, ballStart.position, Quaternion.identity, arenaAnim.transform);
    }

    private void Update()
    {
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


        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateArenaLeft();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RotateArenaRight();
        }
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
    }
}
