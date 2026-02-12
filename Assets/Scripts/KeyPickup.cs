using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyName; // Type "RedKey" or "BlueKey" here to match the door!

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.Instance.AddKey(keyName);

            // Optional: Play your pickup sound here
            // AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject);
        }
    }
}