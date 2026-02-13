using UnityEngine;
using TMPro; // Make sure you have this for TextMeshPro!
using System.Collections;

public class LockedDoor : MonoBehaviour
{
    public string keyNeeded;
    [SerializeField] private AudioClip _unlockSound;

    [Header("UI Feedback")]
    public GameObject unlockText; // Drag your UI Text (the one on the Canvas) here
    public float displayTime = 3f;

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

        // Show the text before the door is destroyed
        if (unlockText != null)
        {
            // We show the text, but since the door is about to be destroyed,
            // we need to make sure the text manages itself or is handled by a manager.
            // A quick trick: Activate it here!
            unlockText.SetActive(true);

            // Start a timer to hide it (we'll use a separate helper or just let it sit)
            StartCoroutine(HideTextAfterDelay());
        }

        // Instead of Destroy(gameObject) immediately, we disable visuals/colliders 
        // so the script can finish the Coroutine.
        StartCoroutine(ProcessDoorOpening());
    }

    private IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        if (unlockText != null) unlockText.SetActive(false);
        Destroy(gameObject); // Now we destroy the door
    }

    private IEnumerator ProcessDoorOpening()
    {
        // Disable the door so player can pass, but keep object alive for the timer
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return null;
    }
}