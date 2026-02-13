using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    [Header("Setup")]
    public int levelNumber = 1;
    public string sceneName;

    [Header("Optional UI")]
    public GameObject lockOverlay;  
    public Button button;


void Reset()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        Refresh();
        button.onClick.AddListener(HandleClick);
    }

    public void Refresh()
    {
        bool unlocked = Progress.IsUnlocked(levelNumber);

        button.interactable = unlocked;

        if (lockOverlay != null)
            lockOverlay.SetActive(!unlocked);
    }

    private void HandleClick()
    {
        if (!Progress.IsUnlocked(levelNumber))
            return;

        SceneManager.LoadScene(sceneName);
    }

}
