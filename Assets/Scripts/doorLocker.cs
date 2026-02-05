using UnityEngine;
using UnityEngine.SceneManagement;

public class doorLocker : MonoBehaviour
{
    public GameObject bottomHatch;
    public GameObject topHatch;

    private Rigidbody2D botHatchrb;
    private Rigidbody2D topHatchrb;

    public float lockSpeed;
    private bool ifLocked = false;
    private float closeTime;
    public float lockDuration = 3.0f;
    public bool doorsClosed = false;


    public GameObject player;

    private void Start()
    {
        botHatchrb = bottomHatch.GetComponent<Rigidbody2D>();
        topHatchrb = topHatch.GetComponent<Rigidbody2D>();

    }

    public void LockDoors()
    {
        ifLocked = true;
    }


    private void FixedUpdate()
    {
        if (ifLocked)
        {
            closeTime += Time.fixedDeltaTime;
            if (closeTime >= 1f)
            {
                if (closeTime < lockDuration)
                {

                    Vector2 newBotPos = botHatchrb.position + Vector2.up * lockSpeed * Time.fixedDeltaTime;
                    botHatchrb.MovePosition(newBotPos);
                    Vector2 newTopPos = topHatchrb.position + Vector2.down * lockSpeed * Time.fixedDeltaTime;
                    topHatchrb.MovePosition(newTopPos);
                }
            }
        }

        if (closeTime >= lockDuration)
        {
            doorsClosed = true;
        }

    }

    
}

