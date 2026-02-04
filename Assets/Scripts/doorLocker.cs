using UnityEngine;

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
        if(ifLocked && closeTime < lockDuration)
        {
            Debug.Log("Doors are locking.");

            closeTime += Time.fixedDeltaTime;

            Vector2 newBotPos = botHatchrb.position + Vector2.up * lockSpeed * Time.fixedDeltaTime;
            botHatchrb.MovePosition(newBotPos);
            Vector2 newTopPos = topHatchrb.position + Vector2.down * lockSpeed * Time.fixedDeltaTime;
            topHatchrb.MovePosition(newTopPos);
        }
    }
}

