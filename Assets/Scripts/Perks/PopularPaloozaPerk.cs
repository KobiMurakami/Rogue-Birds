using System;
using UnityEngine;

public class PopularPaloozaPerk : Perk
{
    //Add another bird to the bag at start of each round
    
   
    
    void Start()
    {
        AbilityGenerator.OnBonusChosen += AbilityGeneratorOnOnBonusChosen;
    }

    private void AbilityGeneratorOnOnBonusChosen()
    {
        BirdBagManager.Instance.AddRandomBirdToBag();
    }


    private void OnDestroy()
    {
        AbilityGenerator.OnBonusChosen -= AbilityGeneratorOnOnBonusChosen;
    }
}
