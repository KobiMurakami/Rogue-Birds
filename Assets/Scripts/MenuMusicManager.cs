using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    public AudioSource menuMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuMusic = GetComponent<AudioSource>();
        menuMusic.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
