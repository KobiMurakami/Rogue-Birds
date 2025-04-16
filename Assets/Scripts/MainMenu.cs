using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button backButton;


    [Header("Menu Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Settings Options")]
    [SerializeField] private Slider musicVolumeSlider;

    void Start()
    {
        //Button Lsiteners
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        } 

        if (backButton != null) 
        {
            backButton.onClick.AddListener(CloseSettings);
        }

        // If settings exists, then we can initialize them
        if (PlayerPrefs.HasKey("Music Volume"))
        {
            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            }
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("Scene");
    }

    void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {   
        //This is where we save the settings
        if (musicVolumeSlider != null)
        {
            PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        }

        PlayerPrefs.Save();

        ApplySettings();

        //Return to menu
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    private void ApplySettings()
    {
        if (AudioManager.Instance != null)
        {
            if(musicVolumeSlider != null)
            {
                AudioManager.Instance.SetMusicVolume(musicVolumeSlider.value);
            }
        }
    }
}
