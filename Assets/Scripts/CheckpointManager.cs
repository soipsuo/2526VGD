using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    // Static so the KillBrick can see it from anywhere
    public static Vector2 lastCheckpointPos;

    void Start()
    {
        // Find the player at the start of the game and save their 
        // starting position as the first 'checkpoint'.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            lastCheckpointPos = player.transform.position;
        }
    }
}