using UnityEngine;
using System.Collections;

public class disappearingPlatforms : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    private bool disappear;
    private float timer;
    public float dissappearStrength = 0.2f;
    [SerializeField] float waitTime;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            disappear = true;
        }
    }

    private void Update()
    {


        if (disappear)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - dissappearStrength);
                timer = 0;
            }
            
        }

        if (spriteRenderer.color.a <= 0)
        {
            Destroy(gameObject);
        }

    }



}
