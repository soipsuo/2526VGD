using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("Pentomino Settings")]
    [Tooltip("Add all Pentominos that need to be active")]
    public List<GameObject> ActivePentominos = new List<GameObject>();

    public float movementFrequency = 1.0f; // Number next to f changes how many seconds it takes to move block         

    private float passedTime = 0;

    void Update()
    {
        // Safety code to avoid crashes
        if (ActivePentominos.Count == 0) return;

        passedTime += Time.deltaTime;

        // Check if it's time to move the blocks
        if (passedTime >= movementFrequency)
        {
            passedTime = 0;
            MoveAllPentominos(Vector3.down);
        }
    }
    void MoveAllPentominos(Vector3 direction)
    {
        foreach (GameObject pentomino in ActivePentominos)
        {
            // Just in case I make dumb mistake
            if (pentomino != null)
            {
                pentomino.transform.position += direction;
            }
        }
    }
}