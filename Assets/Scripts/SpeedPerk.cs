using System;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPerk : MonoBehaviour
{
    private void Start()
    {
        foreach (Bird bird in BirdBagManager.Instance.birdBag)
        {
            bird.speedModifier *= 2;
        }
        
        BirdBagManager.OnBirdAdded += BirdBagManagerOnOnBirdAdded;
        
    }
    
    private void BirdBagManagerOnOnBirdAdded(Bird bird)
    {
        bird.speedModifier *= 2;
    }

    private void OnDestroy()
    {
        foreach (Bird bird in BirdBagManager.Instance.birdBag)
        {
            bird.speedModifier /= 2;
        }
        
        BirdBagManager.OnBirdAdded -= BirdBagManagerOnOnBirdAdded;
    }
}
