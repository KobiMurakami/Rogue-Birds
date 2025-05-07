using System;
using UnityEngine;

public class BouncyBonusPerk : Perk
{
    
    private void Start()
    {
        BouncyBird.OnBouncyBirdKill+= BouncyBirdOnOnBouncyBirdKill;
    }

    private void BouncyBirdOnOnBouncyBirdKill()
    {
        BirdBagManager.Instance.maxRerolls += 1;
    }

    private void OnDestroy()
    {
        BouncyBird.OnBouncyBirdKill -= BouncyBirdOnOnBouncyBirdKill;
    }
}
