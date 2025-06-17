using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel; // Reference to the settings panel GameObject
    private bool isSettingsOpen = false; // Track if the settings panel should be closed
    public AudioMixer audioMixer; // Reference to the AudioMixer for volume control
    List<string> resolutions = new List<string>(); // Array to hold available screen resolutions
    public Dropdown resolutionDropdown; // Dropdown for selecting screen resolution
    public Slider VolumeSlider; // Slider for adjusting the master volume

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resolutionDropdown.ClearOptions(); // Clear existing options in the dropdown

        int currentResolutionIndex = 0; // Variable to track the current resolution index

        Screen.resolutions.ToList().ForEach(res => resolutions.Add(res.ToString())); // Populate the resolutions list with available screen resolutions
        currentResolutionIndex = Screen.resolutions.ToList().IndexOf(Screen.currentResolution); // Find the index of the current resolution

        resolutionDropdown.AddOptions(resolutions); // Add the options to the dropdown

        resolutionDropdown.value = currentResolutionIndex; // Set the dropdown to the current resolution
        resolutionDropdown.RefreshShownValue(); // Refresh the dropdown to show the current value

        float masterVolume = -20f; // Variable to hold the master volume value
        audioMixer.GetFloat("masterVolume", out masterVolume); // Get the current master volume from the audio mixer
        VolumeSlider.value = masterVolume; // Set the slider value to the current master volume

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenLevelSelectionScreen()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void UpdateSettingsPanelStatus()
    {
        isSettingsOpen = !isSettingsOpen;
        settingsPanel.SetActive(isSettingsOpen); // Toggle the settings panel visibility
    }

    public void SetMasterVolume(float masterVolume)
    {
        if (masterVolume <= -50f) masterVolume = -80f;
        audioMixer.SetFloat("masterVolume", masterVolume); // Set the master volume in the audio mixer
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; // Set fullscreen mode based on the toggle state
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex < 0 || resolutionIndex >= Screen.resolutions.Length)
        {
            Debug.LogError("Invalid resolution index selected."); // Log an error if the index is out of bounds
            return;
        }
        Resolution selectedResolution = Screen.resolutions[resolutionIndex]; // Get the selected resolution
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen); // Set the screen resolution
    }
}
