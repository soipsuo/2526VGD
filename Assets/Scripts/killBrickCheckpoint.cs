using UnityEngine;
using System.Collections;

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

            // Turn off movement script immediately
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
                StartCoroutine(ForceZeroRoutine());
            }
        }
    }

    IEnumerator ForceZeroRoutine()
    {
        // 1. Teleport
        Vector3 spawnPos = CheckpointManager.lastCheckpointPos;
        spawnPos.z = 0;
        player.transform.position = spawnPos;
        player.SetActive(true);

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // 2. BRUTE FORCE: Zero out velocity for 0.2 seconds
            // This kills any "ghost" forces or buffered movement
            float forceTime = 0.2f;
            float elapsed = 0f;

            while (elapsed < forceTime)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.position = spawnPos; // Keep them glued to the spot

                elapsed += Time.deltaTime;
                yield return null; // Wait for the next frame
            }
        }

        // 3. Final Sync
        Physics2D.SyncTransforms();

        // 4. Re-enable everything
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