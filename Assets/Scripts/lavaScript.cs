using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class lavaScript : MonoBehaviour
{

    private Rigidbody2D rb;
    private float timer = 0f;
    public float stopTime = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= 3f &&  timer <= stopTime)
        {
            Vector2 newPos = rb.position + Vector2.up * 3f * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }

    }

        
        
 
}
