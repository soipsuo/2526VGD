using UnityEngine;

public class swimmingMovement : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;

    [Header("Movement Settings")]
    public float swimSpeed = 2f;
    public float swimForwardSpeed = 2f;

    [Header("Audio Setup")]
    [SerializeField] private AudioSource _swimSource; // Drag your 15s loop AudioSource here
    [Range(0f, 1f)][SerializeField] private float _volume = 0.5f;

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();

        // Set up the source for looping
        if (_swimSource != null)
        {
            _swimSource.loop = true;
            _swimSource.playOnAwake = false;
            _swimSource.volume = _volume;
        }
    }

    void FixedUpdate()
    {
        Vector2 vel = rb.linearVelocity;

        // Track if any movement key is pressed
        bool isMoving = false;

        if (Input.GetKey(KeyCode.W))
        {
            vel.y = swimSpeed;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vel.y = -swimSpeed;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            vel.x = swimForwardSpeed;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            vel.x = -swimForwardSpeed;
            isMoving = true;
        }

        // Apply friction/stop if no keys are pressed
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) vel.y = 0f;
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) vel.x = 0f;

        rb.linearVelocity = vel;

        HandleAudio(isMoving);
    }

    private void HandleAudio(bool isMoving)
    {
        if (_swimSource == null || _swimSource.clip == null) return;

        if (isMoving)
        {
            if (!_swimSource.isPlaying) _swimSource.Play();
        }
        else
        {
            if (_swimSource.isPlaying) _swimSource.Stop();
        }
    }

    // Stop audio immediately if the script is disabled (e.g., exiting water)
    private void OnDisable()
    {
        if (_swimSource != null && _swimSource.isPlaying) _swimSource.Stop();
    }
}