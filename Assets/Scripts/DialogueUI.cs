using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogueBox;     // Assign the PANEL (parent)
    public TMP_Text dialogueText;      // Assign TMP text inside it

    [Header("Typing")]
    public float charsPerSecond = 35f;
    public KeyCode advanceKey = KeyCode.E;

    [Header("Auto Advance")]
    public bool autoAdvance = true;
    public float autoAdvanceDelay = 1.0f;

    [Header("After Last Line")]
    public float stayTimeAfterLastLine = 0.5f;
    public float fadeDuration = 0.4f;

    private CanvasGroup boxGroup;
    private Coroutine dialogueRoutine;

    private bool isTyping;
    private bool skipRequested;

    public bool IsShowing { get; private set; }

    void Awake()
    {
        if (dialogueBox != null)
            boxGroup = dialogueBox.GetComponent<CanvasGroup>();

        HideInstant();
    }

    void Update()
    {
        if (!IsShowing) return;

        if (Input.GetKeyDown(advanceKey))
        {
            if (isTyping)
            {
                skipRequested = true; // finish line instantly
            }
        }
    }

    public void ShowDialogue(string[] lines)
    {
        if (dialogueRoutine != null)
            StopCoroutine(dialogueRoutine);

        dialogueRoutine = StartCoroutine(RunDialogue(lines));
    }

    IEnumerator RunDialogue(string[] lines)
    {
        IsShowing = true;
        dialogueBox.SetActive(true);

        if (boxGroup != null)
            boxGroup.alpha = 1f;

        for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
        {
            string line = lines[lineIndex];

            dialogueText.text = line;
            dialogueText.maxVisibleCharacters = 0;

            isTyping = true;
            skipRequested = false;

            int total = line.Length;
            float delay = 1f / Mathf.Max(1f, charsPerSecond);

            for (int i = 0; i <= total; i++)
            {
                if (skipRequested)
                {
                    dialogueText.maxVisibleCharacters = total;
                    break;
                }

                dialogueText.maxVisibleCharacters = i;
                yield return new WaitForSeconds(delay);
            }

            isTyping = false;

            // Wait for key press OR auto advance timer
            float timer = 0f;
            bool advanced = false;

            while (!advanced)
            {
                if (Input.GetKeyDown(advanceKey))
                {
                    advanced = true;
                }
                else if (autoAdvance)
                {
                    timer += Time.deltaTime;
                    if (timer >= autoAdvanceDelay)
                        advanced = true;
                }

                yield return null;
            }
        }

        // After last line
        yield return new WaitForSeconds(stayTimeAfterLastLine);

        if (boxGroup != null)
        {
            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                boxGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
                yield return null;
            }

            boxGroup.alpha = 0f;
        }

        HideInstant();
    }

    public void HideInstant()
    {
        if (dialogueRoutine != null)
        {
            StopCoroutine(dialogueRoutine);
            dialogueRoutine = null;
        }

        IsShowing = false;
        isTyping = false;
        skipRequested = false;

        if (dialogueText != null)
        {
            dialogueText.text = "";
            dialogueText.maxVisibleCharacters = 0;
        }

        if (dialogueBox != null)
            dialogueBox.SetActive(false);
    }
}
