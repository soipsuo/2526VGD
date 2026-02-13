using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public int buttonID; // Set this to 1, 2, or 3 in the Inspector
    public SequencePuzzle manager;

    [Header("Feedback")]
    public Color activeColor = Color.green;
    private Color _originalColor;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        if (_sprite != null) _originalColor = _sprite.color;
    }

    // This triggers when the player walks into the button
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            manager.RegisterPress(buttonID);

            // Visual feedback
            if (_sprite != null) StartCoroutine(FlashColor());
        }
    }

    private System.Collections.IEnumerator FlashColor()
    {
        _sprite.color = activeColor;
        yield return new WaitForSeconds(0.5f);
        _sprite.color = _originalColor;
    }
}