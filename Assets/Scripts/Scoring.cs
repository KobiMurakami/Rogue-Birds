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
    public bool initializedScore = false;

    public TextMeshProUGUI ScoreNumberText;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Level Starting, fetching previous score");
        score = PlayerPrefs.GetInt("currentscore", 0);
        initializedScore = true;
        Debug.Log("Previous Score fetched, it was " + score);
        Enemies.OnEnemyDeath += EnemyOnEnemyDeath;

        AbilityGenerator.OnBonusChosen += BonusOnBonusChosen;
    }

    // Update is called once per frame
    void Update()
    {
        if(!initializedScore) {
            score = PlayerPrefs.GetInt("currentscore", 0);
            initializedScore = true;
            Debug.Log("Initialized Score in update method");
        }
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

    void BonusOnBonusChosen() {
        score = PlayerPrefs.GetInt("currentscore", 0);
        initializedScore = true;
    }
}