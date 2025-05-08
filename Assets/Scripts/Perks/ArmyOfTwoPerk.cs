using UnityEngine;

public class ArmyOfTwoPerk : Perk
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BirdBagManager.Instance.maxShots = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
