using UnityEngine;
using TMPro;

public class LockedDoor : MonoBehaviour
{
    public string keyNeeded;
    [SerializeField] private AudioClip _unlockSound;

    [Header("UI Feedback")]
    [Tooltip("Drag the UI Text object here. It will turn on and stay on.")]
    public GameObject unlockText;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CoinManager.Instance.HasKey(keyNeeded))
            {
                // 1. Play unlock sound on the player
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

        if (unlockText != null)
        {
            unlockText.SetActive(true);
            Debug.Log("UI Text should now be visible!"); // Check your Console for this!
        }
        else
        {
            Debug.LogError("The Unlock Text slot is EMPTY on the door!");
        }

        Destroy(gameObject);
    }
}