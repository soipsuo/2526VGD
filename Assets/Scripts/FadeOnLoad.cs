using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1000)]
public class FadeOnLoad : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float holdSeconds = 6f;
    [SerializeField] private float fadeOutDuration = 1f;

    [SerializeField] private string nextSceneName = "MainMenu";

    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Awake()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("SceneFaderSequence: No CanvasGroup found on this object.");
            enabled = false;
            return;
        }

        // Force black BEFORE first frame
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;
    }

    private void Start()
    {
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        // Start invisible
        canvasGroup.alpha = 0f;

        // Fade IN (0 → 1)
        yield return Fade(0f, 1f, fadeInDuration);

        // Hold visible
        yield return new WaitForSecondsRealtime(holdSeconds);

        // Fade OUT (1 → 0)
        yield return Fade(1f, 0f, fadeOutDuration);

        SceneManager.LoadScene(nextSceneName);
    }


    private IEnumerator Fade(float start, float end, float duration)
    {
        if (duration <= 0f)
        {
            canvasGroup.alpha = end;
            yield break;
        }

        float t = 0f;
        canvasGroup.alpha = start;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, t / duration);
            yield return null;
        }

        canvasGroup.alpha = end;
    }
}
