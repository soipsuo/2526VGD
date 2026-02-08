using UnityEngine;

public class swimmingMovement : MonoBehaviour
{

    public GameObject player;
    private Rigidbody2D rb;

    public float swimSpeed = 2f;
    public float swimForwardSpeed = 2f;



    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();

    }

    
    void FixedUpdate()
    {
        
        Vector2 vel = rb.linearVelocity;
        

        if (Input.GetKey(KeyCode.W))
        {
            vel.y = swimSpeed;
            rb.linearVelocity = vel;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vel.y = -swimSpeed;
            rb.linearVelocity = vel;
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            vel.x = swimForwardSpeed;
            rb.linearVelocity = vel;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            vel.x = -swimForwardSpeed;
            rb.linearVelocity = vel;
        }
        else
        {
            vel.y = 0f;
            vel.x = 0f;
            rb.linearVelocity = vel;
        }

    }
}
