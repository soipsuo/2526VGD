using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // This defines the "Instance" that the error was complaining about
    public static CoinManager Instance { get; private set; }

    public int totalCoins = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log("Coins Collected! Total: " + totalCoins);
    }
}