using UnityEngine;

public class swimmingMovement : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; 

    [Header("Movement Settings")]
    public float swimSpeed = 2f;
    public float swimForwardSpeed = 2f;

    [Header("Visuals")]
    public float rotationSpeed = 5f; 

    [Header("Audio Setup")]
    [SerializeField] private AudioSource _swimSource;
    [Range(0f, 1f)][SerializeField] private float _volume = 0.5f;

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        spriteRenderer = player.GetComponent<SpriteRenderer>(); 

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
        bool isMoving = false;

        if (Input.GetKey(KeyCode.W)) { vel.y = swimSpeed; isMoving = true; }
        else if (Input.GetKey(KeyCode.S)) { vel.y = -swimSpeed; isMoving = true; }

        if (Input.GetKey(KeyCode.D)) { vel.x = swimForwardSpeed; isMoving = true; }
        else if (Input.GetKey(KeyCode.A)) { vel.x = -swimForwardSpeed; isMoving = true; }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) vel.y = 0f;
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) vel.x = 0f;

        rb.linearVelocity = vel;

        if (isMoving)
        {
            HandleVisuals(vel);
        }

        HandleAudio(isMoving);
    }

    private void HandleVisuals(Vector2 velocity)
    {
        if (spriteRenderer == null) return;

        if (velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        float angle = Mathf.Atan2(velocity.y, Mathf.Abs(velocity.x)) * Mathf.Rad2Deg;

        if (spriteRenderer.flipX) angle = -angle;

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void HandleAudio(bool isMoving)
    {
        if (_swimSource == null || _swimSource.clip == null) return;
        if (isMoving) { if (!_swimSource.isPlaying) _swimSource.Play(); }
        else { if (_swimSource.isPlaying) _swimSource.Stop(); }
    }

    private void OnDisable()
    {
        if (_swimSource != null && _swimSource.isPlaying) _swimSource.Stop();
        if (player != null) player.transform.rotation = Quaternion.identity;
    }
}