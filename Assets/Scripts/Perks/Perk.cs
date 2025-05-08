using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Perk : MonoBehaviour
{
    public Sprite cardSprite;
    public string perkName;
    public string perkDescription;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Destroy(gameObject);
        }
    }
}
