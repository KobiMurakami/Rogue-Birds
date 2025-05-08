using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button backSettingsButton;
    [SerializeField] private Button backCreditsButton;
    [SerializeField] private Button creditsButton;



    [Header("Menu Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Settings Options")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider fxVolumeSlider;

    void Start()
    {
        //Button Lsiteners
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        creditsButton.onClick.AddListener(OpenCredits);

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        } 

        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false);
        }

        if (backSettingsButton != null) 
        {
            backSettingsButton.onClick.AddListener(CloseSettings);
        }

        if (backCreditsButton != null) 
        {
            backCreditsButton.onClick.AddListener(CloseCredits);
        }


        // If settings exists, then we can initialize them
        if (PlayerPrefs.HasKey("Music Volume"))
        {
            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            }
        }

        if (PlayerPrefs.HasKey("FX Volume"))
        {
            if (fxVolumeSlider != null)
            {
                fxVolumeSlider.value = PlayerPrefs.GetFloat("FXVolume");
            }
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("LoadoutSelection");
    }

  void OpenSettings()
{
    mainMenuPanel.SetActive(false);
    creditsPanel.SetActive(false);
    settingsPanel.SetActive(true);
  
    if (AudioManager.Instance != null)
    {
        if (musicVolumeSlider != null && AudioManager.Instance.GetMusicVolume() >= 0)
        {
            musicVolumeSlider.value = AudioManager.Instance.GetMusicVolume();
        }
        
        if (fxVolumeSlider != null && AudioManager.Instance.GetFXVolume() >= 0)
        {
            fxVolumeSlider.value = AudioManager.Instance.GetFXVolume();
        }
    }
}

    void OpenCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void CloseSettings()
    {   
        //This is where we save the settings
        if (musicVolumeSlider != null)
        {
            PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        }

         if (fxVolumeSlider != null)
        {
            PlayerPrefs.SetFloat("FXVolume", fxVolumeSlider.value);
        }


        PlayerPrefs.Save();

        ApplySettings();

        //Return to menu
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    
    public void CloseCredits()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
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
        
        if(fxVolumeSlider != null)
        {
            AudioManager.Instance.SetFXVolume(fxVolumeSlider.value); 
        }
    }
}
}
