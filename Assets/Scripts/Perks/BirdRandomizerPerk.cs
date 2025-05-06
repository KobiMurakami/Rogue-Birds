using System;
using System.Collections.Generic;
using UnityEngine;

public class BirdRandomizerPerk : Perk
{
    private void Start()
    {
        LevelController.OnLevelFinished += LevelControllerOnOnLevelFinished;
    }

    private void LevelControllerOnOnLevelFinished()
    {
        int bagSize = BirdBagManager.Instance.birdBag.Count;
        bagSize += BirdBagManager.Instance.temporarilyNotInBag.Count;
        List<Bird> newBirds = new List<Bird>();
        for (int i = 0; i < bagSize; i++)
        {
            newBirds.Add(BirdBagManager.Instance.GetRandomBirdType());
        }
        
        BirdBagManager.Instance.birdBag = newBirds;
        BirdBagManager.Instance.temporarilyNotInBag.Clear();
        
    }

    private void OnDestroy()
    {
        LevelController.OnLevelFinished -= LevelControllerOnOnLevelFinished;
    }
}
