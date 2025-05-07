using System.Collections.Generic;
using UnityEngine;

public class BagInitializer : MonoBehaviour
{
    //Non-permanent startup script to initialize the bord bag with entries
    public List<Bird> birdPrefabs;

    public void PopulateBirdBag()
    {
        //Generating bag
        foreach (Bird bird in birdPrefabs)
        {
            BirdBagManager.Instance.AddBird(bird);
        }
    }

    public void SetBirdPrefabs(List<GameObject> selectedBirds)
    {
        foreach(GameObject birdPrefab in selectedBirds)
        {
            birdPrefabs.Add(birdPrefab.GetComponent<Bird>());
        }
    }
}
    


