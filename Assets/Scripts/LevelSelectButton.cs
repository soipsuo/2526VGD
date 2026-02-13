using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Add this for the coin text

public class LevelSelectButton : MonoBehaviour
{
    [Header("Setup")]
    public int levelNumber = 1;
    public string sceneName;

    [Header("UI References")]
    public GameObject lockOverlay;
    public Button button;
    public TextMeshProUGUI coinCountText; // Assign the text child here
    public GameObject coinIcon;           // Assign the coin image here

    void Start()
    {
        button = GetComponent<Button>();
        Refresh();
        button.onClick.AddListener(HandleClick);
    }

    public void Refresh()
    {
        bool unlocked = Progress.IsUnlocked(levelNumber);
        button.interactable = unlocked;

        if (lockOverlay != null)
            lockOverlay.SetActive(!unlocked);

        // Look for the coin (1 = found, 0 = not found)
        int coinFound = PlayerPrefs.GetInt("Level_" + levelNumber + "_CoinFound", 0);

        if (coinIcon != null)
        {
            // If coinFound is 1, show the icon. If 0, hide it.
            coinIcon.SetActive(coinFound == 1);
        }

        // We don't need the text anymore since it's just one coin!
        if (coinCountText != null) coinCountText.gameObject.SetActive(false);
    }

    private void HandleClick()
    {
        if (!Progress.IsUnlocked(levelNumber)) return;
        SceneManager.LoadScene(sceneName);
    }

}