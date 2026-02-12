using UnityEngine;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public int totalCoins = 0;
    public List<string> heldKeys = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); }
        else { Instance = this; }
    }

    // This is the function the error is looking for!
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log("Total Coins: " + totalCoins);
    }

    public void AddKey(string keyName)
    {
        heldKeys.Add(keyName);
    }

    public bool HasKey(string keyName)
    {
        return heldKeys.Contains(keyName);
    }

    public void RemoveKey(string keyName)
    {
        heldKeys.Remove(keyName);
    }
}