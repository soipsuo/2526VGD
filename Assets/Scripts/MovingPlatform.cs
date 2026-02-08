using UnityEngine;
using TarodevController;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public int startingPoint;
    public Transform[] points;

    private Vector2[] _fixedPoints;
    private int _currentIndex;
    private Rigidbody2D _rb;
    private Vector2 _currentVelocity;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        // Capture the positions in world space at the start
        // This prevents the "chasing targets" bug if they are children
        _fixedPoints = new Vector2[points.Length];
        for (int j = 0; j < points.Length; j++)
        {
            _fixedPoints[j] = points[j].position;
        }

        transform.position = _fixedPoints[startingPoint];
        _currentIndex = startingPoint;
    }

    void FixedUpdate()
    {
        if (_fixedPoints == null || _fixedPoints.Length == 0) return;

        Vector2 target = _fixedPoints[_currentIndex];
        Vector2 currentPos = _rb.position;

        // Calculate movement
        Vector2 newPos = Vector2.MoveTowards(currentPos, target, speed * Time.fixedDeltaTime);
        _currentVelocity = (newPos - currentPos) / Time.fixedDeltaTime;

        _rb.MovePosition(newPos);

        if (Vector2.Distance(currentPos, target) < 0.05f)
        {
            _currentIndex++;
            if (_currentIndex >= _fixedPoints.Length) _currentIndex = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                // Ensure we only stick if landing on the top
                if (collision.contacts[0].normal.y < -0.5f)
                {
                    player.PlatformVelocity = _currentVelocity;
                }
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