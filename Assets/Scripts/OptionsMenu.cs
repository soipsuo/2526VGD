using UnityEngine;
using UnityEngine.UI; // Needed for Slider and Button

public class OptionsMenu : MonoBehaviour
{
    [Header("Volume Settings")]
    public Slider volumeSlider;

    [Header("Controls UI")]
    public GameObject controlsImage; // Drag your Controls Image here

    void Start()
    {
        // Set the slider to the current volume on start
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        // Make sure controls are hidden at start
        if (controlsImage != null) controlsImage.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        Debug.Log("Volume set to: " + volume);
    }

    public void ToggleControls()
    {
        if (controlsImage != null)
        {
            // If it's on, turn it off. If it's off, turn it on.
            bool isActive = controlsImage.activeSelf;
            controlsImage.SetActive(!isActive);
        }
    }
}