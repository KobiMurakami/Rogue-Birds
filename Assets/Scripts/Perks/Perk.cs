using System;
using UnityEngine;

public class Perk : MonoBehaviour
{
    public Sprite cardSprite;
    public string perkName;
    public string perkDescription;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
