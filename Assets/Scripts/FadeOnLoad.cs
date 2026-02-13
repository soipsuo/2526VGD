using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOnLoad : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float fadeInDuration = 1f;
    public float holdSeconds = 6f;
    public float fadeOutDuration = 1f;

    public string nextSceneName;

    void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    void Start()
    {
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        yield return Fade(1f, 0f, fadeInDuration);

        yield return new WaitForSecondsRealtime(holdSeconds);

        yield return Fade(0f, 1f, fadeOutDuration);

        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator Fade(float start, float end, float duration)
    {
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
