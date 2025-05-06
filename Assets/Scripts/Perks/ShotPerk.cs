using System;
using System.Collections.Generic;
using UnityEngine;

public class ShotPerk : Perk
{
    //Gain 3 more shots per round
    private void Start()
    {
        BirdBagManager.Instance.maxShots += 3;
    }

    private void OnDestroy()
    {
        BirdBagManager.Instance.maxShots -= 3;
    }
}
