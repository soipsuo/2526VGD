using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuUI;
    public GameObject controlsGuide;

    public static bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }
    private void Awake()
    {
        // This keeps the object alive when changing scenes
        DontDestroyOnLoad(gameObject);
    }
    public void Resume()
    {
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (controlsGuide != null) controlsGuide.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ToggleGuide()
    {
        if (controlsGuide != null)
            controlsGuide.SetActive(!controlsGuide.activeSelf);
    }

    // --- NEW: Function to go back to the Level Selector ---
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Always reset time before changing scenes!
        SceneManager.LoadScene("LevelSelector"); // Make sure this matches your scene name exactly
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}