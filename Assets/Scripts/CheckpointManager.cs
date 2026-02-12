using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    // This stays in memory so any script can see the last position
    public static Vector2 lastCheckpointPos;

    void Start()
    {
        // Set the very first spawn point to where the player starts
        if (lastCheckpointPos == Vector2.zero)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) lastCheckpointPos = player.transform.position;
        }
    }
}