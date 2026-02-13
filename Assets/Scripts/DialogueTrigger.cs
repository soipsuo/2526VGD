using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(2, 6)]
    public string[] lines;

    public bool triggerOnce = true;
    private bool hasTriggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (hasTriggered && triggerOnce) return;

        DialogueUI ui = FindFirstObjectByType<DialogueUI>();

        if (ui != null && !ui.IsShowing)
        {
            ui.ShowDialogue(lines);
        }

        hasTriggered = true;
    }
}
