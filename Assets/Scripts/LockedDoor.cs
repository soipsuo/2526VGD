using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public string keyNeeded;
    [SerializeField] private AudioClip _unlockSound; // Drag unlock sound here

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CoinManager.Instance.HasKey(keyNeeded))
            {
                // Play unlock sound
                AudioSource playerSource = collision.gameObject.GetComponentInChildren<AudioSource>();
                if (playerSource != null && _unlockSound != null)
                {
                    playerSource.PlayOneShot(_unlockSound);
                }

                OpenDoor();
            }
            else
            {
                Debug.Log("Locked! Need: " + keyNeeded);
            }
        }
    }

    void OpenDoor()
    {
        CoinManager.Instance.RemoveKey(keyNeeded);
        Destroy(gameObject);
    }
}