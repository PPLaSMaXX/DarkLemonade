using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class InGameMenu : MonoBehaviour
{
    public GameObject menuCanvas; // Reference to the UI GameObject
    public GameObject settingsPanel; // Reference to the settings panel GameObject
    public GameObject fpsText; // Reference to the TextMeshProUGUI component for displaying the version
    private bool isMenuOpen = false; // Track if the menu is open
    private bool isSettingsOpen = false; // Track if the settings panel should be closed
    public AudioMixer audioMixer; // Reference to the AudioMixer for volume control
    List<string> resolutions = new List<string>(); // Array to hold available screen resolutions
    public Dropdown resolutionDropdown; // Dropdown for selecting screen resolution
    public Slider VolumeSlider; // Slider for adjusting the master volume

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false); // Hide the menu at the start

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuStateController(); // Toggle the menu when Escape is pressed
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            fpsText.SetActive(!fpsText.activeSelf); // Toggle the FPS display when F10 is pressed
        }
    }

    public void Resume()
    {
        MenuStateController(); // Close the menu and resume the game
    }
    private void MenuStateController()
    {
        if (isSettingsOpen)
        {
            settingsPanel.SetActive(false); // Hide the settings panel if it was open
            menuCanvas.SetActive(true); // Hide the menu canvas
            isMenuOpen = true;
            isSettingsOpen = false; // Reset the settings state
        }
        else
        {
            isMenuOpen = !isMenuOpen; // Update the menu state
            menuCanvas.SetActive(isMenuOpen); // Toggle the menu visibility

        }
        if (isMenuOpen)
        {
            Time.timeScale = 0; // Pause the game when the menu is open
        }
        else
        {
            Time.timeScale = 1; // Resume the game when the menu is closed
        }
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1; // Ensure the game is resumed before loading the main menu
        SceneManager.LoadScene("MainMenuScene"); // Load the main menu scene
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

    public void UpdateSettingsPanelStatus()
    {
        isSettingsOpen = !isSettingsOpen;
        settingsPanel.SetActive(isSettingsOpen); // Toggle the settings panel visibility
        menuCanvas.SetActive(!isSettingsOpen); // Hide the menu canvas when settings are open
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