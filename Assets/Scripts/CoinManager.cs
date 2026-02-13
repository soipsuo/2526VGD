using UnityEngine;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public int totalCoins = 0;
    public List<string> heldKeys = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        else { Instance = this; }

        // --- NEW: Load saved coins from memory ---
        totalCoins = PlayerPrefs.GetInt("SavedTotalCoins", 0);
    }

    public int currentLevelNumber; // Set this to 1 in Level 1, 2 in Level 2, etc.

    public void AddCoins(int amount)
    {
        totalCoins += amount;

        // Save to the level-specific slot
        PlayerPrefs.SetInt("Level_" + currentLevelNumber + "_Coins", totalCoins);

        // Also save to the global total if you want
        int globalTotal = PlayerPrefs.GetInt("SavedTotalCoins", 0);
        PlayerPrefs.SetInt("SavedTotalCoins", globalTotal + amount);

        PlayerPrefs.Save();
    }

    // ... Keep your Key functions the same ...
    public void AddKey(string keyName) { heldKeys.Add(keyName); }
    public bool HasKey(string keyName) { return heldKeys.Contains(keyName); }
    public void RemoveKey(string keyName) { heldKeys.Remove(keyName); }
}