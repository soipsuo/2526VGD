using UnityEngine;
using System.Collections.Generic;

public class SequencePuzzle : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [Tooltip("The order of IDs to hit (e.g., 1, 2, 3)")]
    public int[] correctSequence = { 1, 2, 3 };

    [Header("Reward")]
    public GameObject rewardPrefab;
    public Transform spawnPoint;

    private List<int> _playerInput = new List<int>();
    private bool _isSolved = false;

    public void RegisterPress(int id)
    {
        if (_isSolved) return;

        // Add the current button ID to the list
        _playerInput.Add(id);
        Debug.Log("Button " + id + " pressed. Progress: " + _playerInput.Count);

        // Check if the input so far is correct
        for (int i = 0; i < _playerInput.Count; i++)
        {
            if (_playerInput[i] != correctSequence[i])
            {
                Debug.Log("Wrong order! Resetting...");
                _playerInput.Clear();
                return;
            }
        }

        // Check if the sequence is complete
        if (_playerInput.Count == correctSequence.Length)
        {
            SolvePuzzle();
        }
    }

    private void SolvePuzzle()
    {
        _isSolved = true;
        Debug.Log("Puzzle Solved! Spawning reward...");

        if (rewardPrefab != null && spawnPoint != null)
        {
            Instantiate(rewardPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}