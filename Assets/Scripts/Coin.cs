using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // This talks to the Manager script above
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoins(coinValue);
        }

        Destroy(gameObject); // Only the coin disappears
    }
    void Update()
    {
        // Spins the coin 180 degrees per second
        transform.Rotate(Vector3.up * 180 * Time.deltaTime);
    }
}