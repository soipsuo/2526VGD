using UnityEngine;

public static class Progress
{
    private const string KEY = "UnlockedLevel";
    public static int UnlockedLevel
    {
        get => PlayerPrefs.GetInt(KEY, 1);
        set
        {
            PlayerPrefs.SetInt(KEY, Mathf.Max(1, value));
            PlayerPrefs.Save();
        }
    }

    public static bool IsUnlocked(int levelNumber)
    {
        return levelNumber <= UnlockedLevel;
    }

    public static void MarkLevelComplete(int levelJustCompleted)
    {
        int next = levelJustCompleted + 1;

        if (next > UnlockedLevel)
            UnlockedLevel = next;
    }

    public static void ResetProgress()
    {
        UnlockedLevel = 1;
    }
}
