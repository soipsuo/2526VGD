using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType { Coin, Key }
    public ItemType type;

    [Header("Settings")]
    public string keyName = "MainKey"; // Only used if this is a Key
    public int coinValue = 1;         // Only used if this is a Coin

    [Header("Audio")]
    [SerializeField] private AudioClip _pickupSound;
    [Range(0f, 1f)][SerializeField] private float _volume = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Play Sound
            AudioSource playerSource = other.GetComponentInChildren<AudioSource>();
            if (playerSource != null && _pickupSound != null)
            {
                playerSource.PlayOneShot(_pickupSound, _volume);
            }

            // 2. Talk to CoinManager (The new Brain)
            if (CoinManager.Instance != null)
            {
                if (type == ItemType.Coin)
                {
                    CoinManager.Instance.AddCoins(coinValue);
                }
                else if (type == ItemType.Key)
                {
                    CoinManager.Instance.AddKey(keyName);
                }
            }

            // 3. Destroy the item
            Destroy(gameObject);
        }
    }
}