using System;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPerk : Perk
{
    private void Start()
    {
        BirdBagManager.Instance.speedMultiplier *= 2;
        
    }

    private void OnDestroy()
    {
        BirdBagManager.Instance.speedMultiplier /= 2;
    }
}
