using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Scoring : MonoBehaviour
{

    public int numEnemiesInLevel;
    public int numEnemiesKilled;
    public int score;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemies.OnEnemyDeath += EnemyOnEnemyDeath;
    }

    // Update is called once per frame
    void Update()
    {
        if(numEnemiesKilled >= numEnemiesInLevel) {
            Debug.Log("You Win!");
        }
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