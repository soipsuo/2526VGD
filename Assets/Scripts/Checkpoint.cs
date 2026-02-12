using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Update the static respawn position
            CheckpointManager.lastCheckpointPos = transform.position;
            Debug.Log("Checkpoint Saved!");
        }
    }
}