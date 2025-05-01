using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoringOB : MonoBehaviour
{

    public int numEnemiesInLevel;
    public int numEnemiesKilled;
    public int score;
    
    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI highScoreText;
    private int highScore;
    

    void Start()
    {
        Enemies.OnEnemyDeath += EnemyOnEnemyDeath;

        // Load high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
    }

    void Update()
    {
        if (numEnemiesKilled >= numEnemiesInLevel)
        {
            Debug.Log("You Win!");
        }
    }

    void EnemyOnEnemyDeath(string type)
    {
        if (type.Equals("Enemy"))
        {
            score += 100;
            numEnemiesKilled += 1;

            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore); // Save it
            }

            UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        //if (highScoreText != null)
        //highScoreText.text = "High Score: " + highScore;
    }
}
