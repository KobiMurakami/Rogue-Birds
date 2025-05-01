using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Scoring : MonoBehaviour
{

    public int numEnemiesInLevel;
    public int numEnemiesKilled;
    public int score;

    public TextMeshProUGUI ScoreNumberText;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = PlayerPrefs.GetInt("currentScore", 0);
        Enemies.OnEnemyDeath += EnemyOnEnemyDeath;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreNumberText.text = score.ToString();
    }

    void EnemyOnEnemyDeath(String type) {
        if(type.Equals("basic")){
            score += 100;
            numEnemiesKilled += 1;
        }
        else {
            // Other enemy types
        }
    }
}
