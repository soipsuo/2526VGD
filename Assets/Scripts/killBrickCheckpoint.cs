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

            // 1. Disable the movement script so input can't push the player
            // Replace 'PlayerMovement' with the actual name of your movement script
            var movement = player.GetComponent<MonoBehaviour>();
            if (movement != null) movement.enabled = false;

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
        player.SetActive(true);
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // 2. HARD RESET: Freeze the body completely
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            // 3. TELEPORT: Match positions exactly
            Vector3 spawnPos = CheckpointManager.lastCheckpointPos;
            spawnPos.z = 0;

            rb.position = spawnPos;
            player.transform.position = spawnPos;
        }

        Physics2D.SyncTransforms();

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.linearVelocity = Vector2.zero; // Re-zeroing after switching to Dynamic
        }

        // 4. RE-ENABLE MOVEMENT: Only after physics is settled
        var movement = player.GetComponent<MonoBehaviour>();
        if (movement != null) movement.enabled = true;

        playerCollided = false;
        timer = 0f;
        killUI.SetActive(false);
        killnumbers1.SetActive(false);
        killnumbers2.SetActive(false);
        killnumbers3.SetActive(false);
    }
}