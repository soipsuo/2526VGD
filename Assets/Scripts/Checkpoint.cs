using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Update the global position
            CheckpointManager.lastCheckpointPos = transform.position;
            Debug.Log("Checkpoint Saved to Global Manager!");
        }
    }
}