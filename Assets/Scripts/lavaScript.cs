using UnityEngine;
using UnityEngine.UIElements;

public class lavaScript : MonoBehaviour
{

    private Rigidbody2D rb;
    private float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= 3f)
        {
            Vector2 newPos = rb.position + Vector2.up * 2f * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }

    }

        
        
 
}
