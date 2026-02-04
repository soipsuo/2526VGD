using UnityEngine;
using System.Collections;

public class PlayerFly : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private Animator _anim; // Added reference

    public float flapStrength = 8f;
    public float forwardSpeed = 3f;
    private bool flightready = true;

    public float flapTime;
    private bool flapQueued = false;

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        // Grab the animator from the player (or its child visuals)
        _anim = player.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 1. Detect the "Press" (Flap starts)
        if (Input.GetKeyDown(KeyCode.Space) && flightready)
        {
            flapQueued = true;
            flightready = false;

            if (_anim != null) _anim.SetBool("isFlapping", true); // Turn on wings-up

            StartCoroutine(flapCooldown());
        }

        // 2. Detect the "Release" (Go back to gliding)
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (_anim != null) _anim.SetBool("isFlapping", false); // Turn off wings-up
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