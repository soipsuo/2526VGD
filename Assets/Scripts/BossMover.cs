using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMover : MonoBehaviour
{
    public Transform[] nodes;
    public float speed = 3f;

    public BoxCollider2D destroyCollider;
    private int currentIndex = 0;

    public GameObject player;

    private float timer;

    private bool playerDead = false;

    void Update()
    {
        if (currentIndex >= nodes.Length) return;

        Transform target = nodes[currentIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            ColliderNode shape = target.GetComponent<ColliderNode>();
            if (shape != null)
            {
                destroyCollider.size = shape.colliderSize;
                destroyCollider.offset = shape.colliderOffset;
            }

            currentIndex++;
        }

        if(playerDead)
        {
            timer += Time.deltaTime;
            if(timer >= 2f)
            {
                // Restart the scene or show game over screen
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("destoryPlatforms"))
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Player"))
        {
            Destroy(player);
            playerDead = true;
        }
    }

    
}
