using UnityEngine;

public class doorScript : MonoBehaviour
{
    private Vector2 initialPosition;
    public GameObject door;
    private bool playerTouched = false;
    private bool playerTouchedOnce = false;
    private Rigidbody2D rb;
    private float waitTime;
    public float ascendTime;


    private void Start()
    {
        rb = door.transform.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (playerTouched)
        {
            waitTime += Time.fixedDeltaTime;
            if (waitTime >= 1f)
            {
                if (waitTime < ascendTime)
                {
                    Vector2 newPos = rb.position + Vector2.up * 2.5f * Time.fixedDeltaTime;
                    rb.MovePosition(newPos);
                } 
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {

            if (!playerTouched && !playerTouchedOnce)
            {
                playerTouchedOnce = true;
                playerTouched = true;
            }
        }
    }





}
