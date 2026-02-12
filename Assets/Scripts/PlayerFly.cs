using UnityEngine;
using System.Collections;

public class PlayerFly : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private Animator _anim;

    [Header("Flight Settings")]
    public float flapStrength = 8f;
    public float forwardSpeed = 3f;
    private bool flightready = true;
    public float flapTime;
    private bool flapQueued = false;

    [Header("Audio Setup")]
    [SerializeField] private AudioSource _sfxSource; // Drag your SFX AudioSource here
    [SerializeField] private AudioClip _flapSound;   // Drag your flap SFX file here
    [Range(0f, 1f)][SerializeField] private float _flapVolume = 0.5f;

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        _anim = player.GetComponentInChildren<Animator>();

        // Optional: Auto-find the SFX source if you forgot to drag it in
        if (_sfxSource == null) _sfxSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 1. Detect the "Press" (Flap starts)
        if (Input.GetKeyDown(KeyCode.Space) && flightready)
        {
            flapQueued = true;
            flightready = false;

            if (_anim != null) _anim.SetBool("isFlapping", true);

            // PLAY THE FLAP SOUND
            if (_sfxSource != null && _flapSound != null)
            {
                _sfxSource.PlayOneShot(_flapSound, _flapVolume);
            }

            StartCoroutine(flapCooldown());
        }

        // 2. Detect the "Release" (Go back to gliding)
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (_anim != null) _anim.SetBool("isFlapping", false);
        }
    }

    void FixedUpdate()
    {
        // Note: Using rb.velocity as "linearVelocity" is specific to newer Unity versions (2023+)
        // If you get an error here, change it back to rb.velocity
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