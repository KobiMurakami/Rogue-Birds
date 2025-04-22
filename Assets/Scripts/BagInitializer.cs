using System.Collections.Generic;
using UnityEngine;

public class BagInitializer : MonoBehaviour
{
    //Non-permanent startup script to initialize the bord bag with entries
    public List<Bird> birdPrefabs;

    private void Start()
    {
        //Generating bag
        foreach (Bird bird in birdPrefabs)
        {
            BirdBagManager.Instance.AddBird(bird);
        }
    }
}
    


