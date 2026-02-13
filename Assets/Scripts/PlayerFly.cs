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
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioClip _flapSound;
    [Range(0f, 1f)][SerializeField] private float _flapVolume = 0.5f;

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        _anim = player.GetComponentInChildren<Animator>();


        if (_sfxSource == null) _sfxSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && flightready)
        {
            flapQueued = true;
            flightready = false;

            if (_anim != null) _anim.SetBool("isFlapping", true);

            if (_sfxSource != null && _flapSound != null)
            {
                _sfxSource.PlayOneShot(_flapSound, _flapVolume);
            }

            StartCoroutine(flapCooldown());
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (_anim != null) _anim.SetBool("isFlapping", false);
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