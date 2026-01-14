using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]

public class MovingPlatform : MonoBehaviour

{

    public float speed = 2f;

    public int startingPoint;

    public Transform[] points;



    private Vector2[] worldPoints;

    private int i;

    private Rigidbody2D rb;



    void Start()

    {

        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;



        // Cache world-space positions

        worldPoints = new Vector2[points.Length];

        for (int j = 0; j < points.Length; j++)

        {

            worldPoints[j] = points[j].position;

        }



        i = startingPoint;

        rb.position = worldPoints[i];

    }



    void FixedUpdate()

    {

        Vector2 target = worldPoints[i];

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        rb.MovePosition(newPos);



        if (Vector2.Distance(rb.position, target) < 0.02f)

        {

            i++;

            if (i >= worldPoints.Length)

                i = 0;

        }

    }



    // Player sticks to platform

    void OnCollisionEnter2D(Collision2D collision)

    {

        if (collision.gameObject.CompareTag("Player"))

        {

            collision.transform.SetParent(transform);

        }

    }



    void OnCollisionExit2D(Collision2D collision)

    {

        if (collision.gameObject.CompareTag("Player"))

        {

            collision.transform.SetParent(null);

        }

    }

}