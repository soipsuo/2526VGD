using UnityEngine;
using TarodevController;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public int startingPoint;
    public Transform[] points;

    private int i;
    private Rigidbody2D rb;
    private Vector2 currentVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.position = points[startingPoint].position;
        i = startingPoint;
    }

    void FixedUpdate()
    {
        Vector2 target = points[i].position;
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        if (Vector2.Distance(transform.position, target) < 0.05f)
        {
            i++;
            if (i >= points.Length) i = 0;

            target = points[i].position;
            direction = (target - (Vector2)transform.position).normalized;
        }

        currentVelocity = direction * speed;

        rb.MovePosition(rb.position + currentVelocity * Time.fixedDeltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.PlatformVelocity = currentVelocity;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.PlatformVelocity = Vector2.zero;
            }
        }
    }
}