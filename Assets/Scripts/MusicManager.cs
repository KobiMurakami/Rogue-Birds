using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource abilityMusic;
    public AudioSource battleMusic;
    public GameObject abilityScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abilityMusic = GetComponent<AudioSource>();
        abilityMusic.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckAbilityScreen()
    {
        if(abilityScreen.activeSelf == false)
        {
            BattleMusicStart();
        }
    }

    void BattleMusicStart()
    { 
        //Debug Message
        Debug.Log("Guitar player has been executed");
        
        //Stop playing clip
        abilityMusic.Stop();
            
        //Plays battle music
        //battleMusic = GetComponent<AudioSource>();
        battleMusic.Play(0);
    }
}
