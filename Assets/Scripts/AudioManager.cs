using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Initialize audio source if not assigned
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = true;
        }
        
        // Load saved volume settings
        LoadVolumeSettings();
    }
    
    private void Start()
    {
        if (menuMusic != null)
        {
            PlayMusic(menuMusic);
        }
    }
    
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip && musicSource.isPlaying)
            return;
            
        musicSource.clip = clip;
        musicSource.Play();
    }
    
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
    
    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            //Default volume for now
            musicSource.volume = 1.0f;
        }
    }
}