using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speaker;
        [TextArea(2, 6)]
        public string text;
    }

    [Header("UI")]
    public CanvasGroup cutsceneGroup;
    public TMP_Text speakerText;
    public TMP_Text dialogueText;
    public GameObject continuePrompt;

    [Header("Dialogue")]
    public DialogueLine[] lines;

    [Header("Timing")]
    public float fadeDuration = 1.0f;
    public float secondsPerChar = 0.03f;
    public float endDelay = 1.0f;

    private bool fastForward = false;
    private bool lineFinished = false;
    private bool advanceRequested = false;

    void Start()
    {
        if (continuePrompt != null) continuePrompt.SetActive(false);
        StartCoroutine(RunCutscene());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (!lineFinished)
                fastForward = true;
            else
                advanceRequested = true;
        }
    }

    IEnumerator RunCutscene()
    {
        yield return StartCoroutine(FadeIn(cutsceneGroup, fadeDuration));

        for (int i = 0; i < lines.Length; i++)
        {
            yield return StartCoroutine(PlayLine(lines[i]));
        }

        if (continuePrompt != null) continuePrompt.SetActive(false);

        yield return new WaitForSeconds(endDelay);

        yield return StartCoroutine(FadeOut(cutsceneGroup, fadeDuration));

        LoadNextSceneInBuild();
    }

    IEnumerator PlayLine(DialogueLine line)
    {
        if (continuePrompt != null) continuePrompt.SetActive(false);

        if (speakerText != null)
            speakerText.text = line.speaker;

        dialogueText.text = line.text;
        dialogueText.maxVisibleCharacters = 0;

        fastForward = false;
        lineFinished = false;
        advanceRequested = false;

        int total = line.text.Length;
        int visible = 0;

        while (visible < total)
        {
            if (fastForward)
            {
                dialogueText.maxVisibleCharacters = total;
                break;
            }

            visible++;
            dialogueText.maxVisibleCharacters = visible;
            yield return new WaitForSeconds(secondsPerChar);
        }

        dialogueText.maxVisibleCharacters = total;
        lineFinished = true;
        fastForward = false;

        if (continuePrompt != null) continuePrompt.SetActive(true);

        while (!advanceRequested)
            yield return null;
    }

    IEnumerator FadeIn(CanvasGroup cg, float duration)
    {
        cg.alpha = 0f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0f, 1f, t / duration);
            yield return null;
        }

        cg.alpha = 1f;
    }

    IEnumerator FadeOut(CanvasGroup cg, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(1f, 0f, t / duration);
            yield return null;
        }

        cg.alpha = 0f;
    }

    void LoadNextSceneInBuild()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
            nextIndex = 0;

        SceneManager.LoadScene(nextIndex);
    }
}
