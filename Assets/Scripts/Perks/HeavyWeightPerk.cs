using System;
using UnityEngine;

public class HeavyWeightPerk : Perk
{
    //Adds 5% more weight on each enemy killed
    void Start()
    {
        Enemies.OnEnemyDeath += EnemiesOnOnEnemyDeath;
    }

    private void EnemiesOnOnEnemyDeath(string type)
    {
        BirdBagManager.Instance.massMultiplier += .05f;
    }

    private void OnDestroy()
    {
        Enemies.OnEnemyDeath -= EnemiesOnOnEnemyDeath;
    }
}
