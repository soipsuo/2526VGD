using UnityEngine;
using UnityEngine.SceneManagement;

public class killBrick : MonoBehaviour
{
    private GameObject player;
    public GameObject killUI;
    public GameObject killnumbers1;
    public GameObject killnumbers2;
    public GameObject killnumbers3;
    public float waitTime;
    private float timer = 0f;
    private bool playerCollided = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            Debug.Log("Player collided with kill brick.");
            player.SetActive(false);
            killUI.SetActive(true);
            playerCollided = true;


        }
    }

    private void Update()
    {
        if (playerCollided)
        {
            timer += Time.deltaTime;
            if (timer >= 1f && !killnumbers1.activeSelf)
            {
                killnumbers1.SetActive(true);
            }

            if (timer >= 2f && !killnumbers2.activeSelf)
            {
                killnumbers2.SetActive(true);
            }

            if (timer >= 3f && !killnumbers3.activeSelf)
            {
                killnumbers3.SetActive(true);
            }
            if (timer >= waitTime)
            {
                timer = 0f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
