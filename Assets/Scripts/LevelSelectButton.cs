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

        // --- NEW: Display coins for this specific level ---
        // We look for a save key unique to this level number
        int coinsCollected = PlayerPrefs.GetInt("Level_" + levelNumber + "_Coins", 0);

        if (coinCountText != null)
        {
            coinCountText.text = coinsCollected.ToString();
            // Optional: Hide the icon/text if 0 coins collected
            bool hasCoins = coinsCollected > 0;
            if (coinIcon != null) coinIcon.SetActive(hasCoins);
            coinCountText.gameObject.SetActive(hasCoins);
        }
    }

    private void HandleClick()
    {
        if (!Progress.IsUnlocked(levelNumber)) return;
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        Progress.ResetProgress(); // For testing: reset progress every frame (remove in production)
    }

}