using UnityEngine;
using UnityEngine.SceneManagement;

public class swimminGateTrigger : MonoBehaviour
{
    public float resetTime = 4f;

    public GameObject killUI;
    public GameObject killnumbers1;
    public GameObject killnumbers2;
    public GameObject killnumbers3;

    public float timer = 0f;

    doorLocker doorLockerScript;

    private Rigidbody2D playerRb;

    public GameObject player;

    private bool playerFailed = true;


    void Start()
    {
        doorLockerScript = GetComponentInParent<doorLocker>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerFailed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doorLockerScript.doorsClosed && playerFailed)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            timer += Time.deltaTime;
            killUI.SetActive(true);

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
            if (timer >= resetTime)
            {
                timer = 0f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
