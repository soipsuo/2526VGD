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

            // 1. Hide the player and show the Death UI
            player.SetActive(false);
            killUI.SetActive(true);
        }
    }

    private void Update()
    {
        if (playerCollided)
        {
            timer += Time.deltaTime;

            // 2. Simple countdown logic
            if (timer >= 1f) killnumbers1.SetActive(true);
            if (timer >= 2f) killnumbers2.SetActive(true);
            if (timer >= 3f) killnumbers3.SetActive(true);

            // 3. Instead of reloading the scene, we Respawn
            if (timer >= waitTime)
            {
                RespawnPlayer();
            }
        }
    }

    void RespawnPlayer()
    {
        // Move the player to the static checkpoint position
        player.transform.position = CheckpointManager.lastCheckpointPos;

        // Reset variables and UI
        playerCollided = false;
        timer = 0f;

        killUI.SetActive(false);
        killnumbers1.SetActive(false);
        killnumbers2.SetActive(false);
        killnumbers3.SetActive(false);

        // Bring the player back
        player.SetActive(true);
    }
}