using System;
using System.Collections.Generic;
using UnityEngine;

public class DoublePrickPerk : Perk
{
    private void Start()
    {
        BlowfishBird.OnBlowjayAbillity += BlowfishBirdOnOnBlowjayAbillity;
    }

    private void BlowfishBirdOnOnBlowjayAbillity(BlowfishBird bird)
    {
        bird.numberOfNeedles *= 2;
    }

    private void OnDestroy()
    {
        BlowfishBird.OnBlowjayAbillity -= BlowfishBirdOnOnBlowjayAbillity;
    }
}
