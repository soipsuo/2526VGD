using UnityEngine;

public class KillBrickCheckpoint : MonoBehaviour
{
    [Header("UI References")]
    public GameObject killUI;
    public GameObject killnumbers1, killnumbers2, killnumbers3;

    [Header("Settings")]
    public float waitTime = 4f;

    private GameObject player;
    private float timer = 0f;
    private bool playerCollided = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerCollided)
        {
            player = collision.gameObject;
            playerCollided = true;

            // Hide the player and show the Death UI
            player.SetActive(false);
            killUI.SetActive(true);
        }
    }

    private void Update()
    {
        if (playerCollided)
        {
            timer += Time.deltaTime;

            if (timer >= 1f) killnumbers1.SetActive(true);
            if (timer >= 2f) killnumbers2.SetActive(true);
            if (timer >= 3f) killnumbers3.SetActive(true);

            if (timer >= waitTime)
            {
                RespawnPlayer();
            }
        }
    }

    void RespawnPlayer()
    {
        // 1. Reset Physics using modern linearVelocity API
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // linearVelocity replaces the old .velocity warning
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // 2. Set Position (Z at 0 prevents the player spawning behind the background)
        Vector3 spawnPos = CheckpointManager.lastCheckpointPos;
        spawnPos.z = 0;
        player.transform.position = spawnPos;

        // Reset variables and UI
        playerCollided = false;
        timer = 0f;

        killUI.SetActive(false);
        killnumbers1.SetActive(false);
        killnumbers2.SetActive(false);
        killnumbers3.SetActive(false);

        // 3. Bring the player back
        player.SetActive(true);
    }
}