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
        // 1. Bring the player back so we can access the Rigidbody
        player.SetActive(true);

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // 2. STOP PHYSICS: Switch to Kinematic so it doesn't move or fall
            rb.bodyType = RigidbodyType2D.Kinematic;

            // Clear all current momentum
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            // 3. TELEPORT: Move the physics body directly
            Vector3 spawnPos = CheckpointManager.lastCheckpointPos;
            spawnPos.z = 0;

            rb.position = spawnPos; // Move physics position
            player.transform.position = spawnPos; // Move visual position
        }

        // 4. SYNC: Force Unity to update the teleported position immediately
        Physics2D.SyncTransforms();

        if (rb != null)
        {
            // 5. RESUME PHYSICS: Set back to Dynamic and ensure velocity is still zero
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.linearVelocity = Vector2.zero;
        }

        // Reset variables and UI
        playerCollided = false;
        timer = 0f;

        killUI.SetActive(false);
        killnumbers1.SetActive(false);
        killnumbers2.SetActive(false);
        killnumbers3.SetActive(false);
    }
}