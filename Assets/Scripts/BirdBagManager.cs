
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BirdBagManager : MonoBehaviour
{
    public static BirdBagManager Instance { get; set; }


    public float speedMultiplier;
    public float massMultiplier;
    public int maxRerolls;
    public int maxShots;
    
    public List<Bird> allBirdTypes = new List<Bird>();
    public List<Bird> birdBag = new List<Bird>();
    public List<Bird> temporarilyNotInBag = new List<Bird>();
    public int maxBirds = 5;
    
    //Events
    public delegate void BirdAdded(Bird newBird);
    public static event BirdAdded OnBirdAdded;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    //Add a bird to the total bag
    public void AddBird(Bird bird)
    {

        if(birdBag.Count < maxBirds)
        {
           birdBag.Add(bird);
           OnBirdAdded?.Invoke(bird); 
        }
        
    }
    
    //Remove a bird from the total bag
    public void RemoveBird(Bird bird)
    {
        //Sanity check
        if (bird != null && birdBag.Contains(bird))
        {
            birdBag.Remove(bird);
        }
        else
        {
            Debug.LogWarning("Attempted to remove a bird that is either null or not in the bag!");
        }
    }
    
    //Remove a bird from the total bag by name
    public void RemoveBirdByName(string birdName)
    {
        //Sanity check
        if (string.IsNullOrEmpty(birdName))
        {
            Debug.LogWarning("Bird name is null or empty.");
            return;
        }

        Bird birdToRemove = birdBag.Find(b => b != null && b.name == birdName);

        if (birdToRemove != null)
        {
            birdBag.Remove(birdToRemove);
        }
        else
        {
            Debug.LogWarning("No bird with the name '" + birdName + "' was found in the bag.");
        }
    }
    
    //Returns the script object of a random bird from the bag, updates temporarilyNotInBag
    public Bird GetBirdForShooting()
    {
        //Sanity check, player should lose if this triggers while firing birds
        if (birdBag.Count == 0)
        {
            Debug.LogWarning("No birds left in the bag!");
            //Call End game function
            return null;
        }
        
        int birdIndex = Random.Range(0, birdBag.Count);
        Bird returnBird = birdBag[birdIndex];
        birdBag.RemoveAt(birdIndex);
        
        temporarilyNotInBag.Add(returnBird);
        return returnBird;
    }
    
    //Returns the script object of a random bird from the bag, does NOT update temporarilyNotInBag or totalBag
    public Bird GetRandomBird()
    {
        //Sanity check
        if (birdBag.Count == 0)
        {
            Debug.LogWarning("No birds left in the bag!");
            return null;
        }
        
        int birdIndex = Random.Range(0, birdBag.Count);
        Bird returnBird = birdBag[birdIndex];
        return returnBird;
    }
    
    //Resets bag to original state, ------NEEDS TO BE CALLED WHEN ROUND IS OVER-----
    public void ResetBag()
    {
        foreach (Bird bird in temporarilyNotInBag)
        {
            birdBag.Add(bird);
        }
        temporarilyNotInBag.Clear();
    }

    public void ClearBag()
    {
        birdBag.Clear();
    }
    
    //Returns a String list off all unique birds in the bag, -----USE THIS BEFORE REMOVING A BIRD-----
    public List<string> GetUniqueBirdNames()
    {
        HashSet<string> uniqueNames = new HashSet<string>();

        foreach (Bird bird in birdBag)
        {
            if (bird != null)
            {
                uniqueNames.Add(bird.name);
            }
        }

        return new List<string>(uniqueNames);
    }

    public void ReplaceBird(Bird bird)
    {
        //Sanity check
        if (bird != null && temporarilyNotInBag.Contains(bird))
        {
            temporarilyNotInBag.Remove(bird);
            birdBag.Add(bird);
        }
        else
        {
            Debug.LogWarning("Attempted to remove a bird that is either null or not in the bag!");
        }
    }

    //Remove random bird in bag
    public void RemoveRandomBirdFromBag(){
        birdBag.Remove(GetRandomBird());
    }

    //---------------New Stuff for between rounds---------------------
    //Gets a random type of bird
    public Bird GetRandomBirdType(){
        return allBirdTypes[Random.Range(0, allBirdTypes.Count)];
    }

    //Adds random bird from total pool to bag
    public void AddRandomBirdToBag(){
        AddBird(GetRandomBirdType());
    }
}
