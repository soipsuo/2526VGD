using UnityEngine;
using UnityEngine.SceneManagement;

public class roomTeleport : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    public float scaleRate = -0.1f;
    public float scaleTime = 3f;
    private bool playerTouched = false;
    private float timer = 0f;
    private bool scaleDone = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            rb = player.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            playerTouched = true;
            
        }
    }

    private void FixedUpdate()
    {
        if (playerTouched)
        {
            timer += Time.fixedDeltaTime;
            if (timer <= scaleTime)
            {
                Vector3 newScale = player.transform.localScale;
                newScale += new Vector3(scaleRate * Time.deltaTime, scaleRate * Time.deltaTime, scaleRate * Time.deltaTime);
                player.transform.localScale = newScale;
                
            } else if (timer > scaleTime && !scaleDone)
            {
                scaleDone = true;
                Debug.Log("Teleporting to Room2");
                // SceneManager.LoadScene("Room2");
            }

        }

    }



}


