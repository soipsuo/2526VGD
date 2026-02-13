using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Pentomino Settings")]
    public List<GameObject> ActivePentominos = new List<GameObject>();

    public float MovementFrequency = 1.0f; // Number next to f changes how many seconds it takes to move block         

    private float passedTime = 0;

    void Update()
    {
        // Safety code to avoid crashes
        if (ActivePentominos.Count == 0) return;

        passedTime += Time.deltaTime;

        // Check if it's time to move the blocks
        if (passedTime >= MovementFrequency)
        {
            passedTime = 0;
            MoveAllPentominos(Vector3.down);
        }
    }
    void MoveAllPentominos(Vector3 direction)
    {
        foreach (GameObject pentomino in ActivePentominos)
        {
            if (pentomino != null)
            {
                pentomino.transform.position += direction;
            }
        }
    }
}