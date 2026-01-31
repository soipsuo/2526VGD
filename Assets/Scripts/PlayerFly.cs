using UnityEngine;
using System.Collections;


public class PlayerFly : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;

    public float flapStrength = 8f;
    public float forwardSpeed = 3f;
    private bool flightready = true;

    public float flapTime;
    private bool flapQueued = false;

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && flightready)
        {
            flapQueued = true;
            flightready = false;
            StartCoroutine(flapCooldown());
        }
    }

    void FixedUpdate()
    {
        Vector2 vel = rb.linearVelocity;

        vel.x = forwardSpeed;

        if (flapQueued)
        {
            vel.y = flapStrength;
            flapQueued = false;
        }

        rb.linearVelocity = vel;
    }

    private IEnumerator flapCooldown()
    {
        yield return new WaitForSeconds(flapTime);
        flightready = true;
    }
}
