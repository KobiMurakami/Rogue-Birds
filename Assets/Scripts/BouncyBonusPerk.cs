using System;
using UnityEngine;

public class BouncyBonusPerk : MonoBehaviour
{
    
    private void Start()
    {
        BouncyBird.OnBouncyBirdKill+= BouncyBirdOnOnBouncyBirdKill;
    }

    private void BouncyBirdOnOnBouncyBirdKill()
    {
        //TODO add a reroll, ----REROLL AND SHOTS LEFT NEED TO BE MOVED OFF OF SLINGSHOT AND ON TO THE BAG 
    }

    private void OnDestroy()
    {
        BouncyBird.OnBouncyBirdKill -= BouncyBirdOnOnBouncyBirdKill;
    }
}
