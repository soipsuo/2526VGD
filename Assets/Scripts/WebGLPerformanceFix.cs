using UnityEngine;

public class WebGLPerformanceFix : MonoBehaviour
{
    private static bool alreadyExists = false;

    void Awake()
    {
        if (alreadyExists)
        {
            Destroy(gameObject);
            return;
        }

        alreadyExists = true;
        DontDestroyOnLoad(gameObject);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}
