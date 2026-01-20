using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
 public void OnStartClick()
    {
        SceneManager.LoadScene("LevelSelector");
    }
 public void OnSettingsClick()
    {
        SceneManager.LoadScene("Settings");
    }
 public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
