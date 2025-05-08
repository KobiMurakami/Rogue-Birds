using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource fxSource;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip buttonClickSound;
    
    [Header("UI Controls")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider fxVolumeSlider;
    
    private bool _updatingMusicSlider = false;
    private bool _updatingFXSlider = false;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        LoadVolumeSettings();
    }
    
    private void Start()
    {
        if (menuMusic != null)
        {
            PlayMusic(menuMusic);
        }
        
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.RemoveAllListeners();
        }
        
        if (fxVolumeSlider != null)
        {
            fxVolumeSlider.onValueChanged.RemoveAllListeners();
        }
        
        Invoke("SetupSliders", 0.1f);
    }
    
    private void SetupSliders()
    {
        if (musicVolumeSlider != null)
        {
            _updatingMusicSlider = true;
            musicVolumeSlider.value = musicSource.volume;
            _updatingMusicSlider = false;
            
            musicVolumeSlider.onValueChanged.AddListener(MusicSliderChanged);
            
            Debug.Log("Music slider initialized with value: " + musicVolumeSlider.value);
        }
        
        if (fxVolumeSlider != null)
        {
            _updatingFXSlider = true;
            fxVolumeSlider.value = fxSource.volume;
            _updatingFXSlider = false;
            
            fxVolumeSlider.onValueChanged.AddListener(FXSliderChanged);
            
            Debug.Log("FX slider initialized with value: " + fxVolumeSlider.value);
        }
        
    }
    
    private void PlayTestSound()
    {
        if (buttonClickSound != null)
        {
            PlayFX(buttonClickSound);
        }
    }
    
    private void MusicSliderChanged(float value)
    {
        if (_updatingMusicSlider) return;
        
        SetMusicVolume(value);
        Debug.Log("Music slider changed to: " + value);
    }
    
    private void FXSliderChanged(float value)
    {
        if (_updatingFXSlider) return;
        
        SetFXVolume(value);
        Debug.Log("FX slider changed to: " + value);
    }
    
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null) return;
        
        musicSource.clip = clip;
        musicSource.Play();
    }
    
    public void PlayFX(AudioClip clip)
    {
        if (fxSource == null || clip == null) return;
        
        fxSource.PlayOneShot(clip);
        Debug.Log("Playing FX with volume: " + fxSource.volume);
    }
    
    public void PlayButtonClickSound()
    {
        PlayFX(buttonClickSound);
    }
    
    public void SetMusicVolume(float volume)
    {
        if (musicSource == null) return;
        
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
        
        if (musicVolumeSlider != null && Mathf.Abs(musicVolumeSlider.value - volume) > 0.01f)
        {
            _updatingMusicSlider = true;
            musicVolumeSlider.value = volume;
            _updatingMusicSlider = false;
        }
        
        Debug.Log("Set music volume to: " + volume);
    }
    
    public void SetFXVolume(float volume)
    {
        if (fxSource == null) return;
        
        fxSource.volume = volume;
        PlayerPrefs.SetFloat("FXVolume", volume);
        PlayerPrefs.Save();
        
        // Update slider if needed
        if (fxVolumeSlider != null && Mathf.Abs(fxVolumeSlider.value - volume) > 0.01f)
        {
            _updatingFXSlider = true;
            fxVolumeSlider.value = volume;
            _updatingFXSlider = false;
        }
        
        Debug.Log("Set FX volume to: " + volume);
    }
    
    private void LoadVolumeSettings()
    {
        float defaultMusicVolume = 0.75f;
        float defaultFXVolume = 1.0f;
        
        musicSource.volume = defaultMusicVolume;
        fxSource.volume = defaultFXVolume;
        
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
            Debug.Log("Loaded saved music volume: " + musicSource.volume);
        }
        
        if (PlayerPrefs.HasKey("FXVolume"))
        {
            fxSource.volume = PlayerPrefs.GetFloat("FXVolume");
            Debug.Log("Loaded saved FX volume: " + fxSource.volume);
        }
    }
    public float GetMusicVolume()
    {
      return musicSource != null ? musicSource.volume : -1;
    }

    public float GetFXVolume()
    {
        return fxSource != null ? fxSource.volume : -1;
    }
}