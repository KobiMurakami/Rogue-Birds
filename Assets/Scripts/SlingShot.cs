using UnityEngine;

public class SlingShot : MonoBehaviour
{
    //This script should be the same as the one used for slingshot movement, attached to the slingshot gameobject
    
    public Bird activeBird;

    void Start()
    {
        //Not permanent code, shouldn't be in start prolly
        activeBird = BirdBagManager.Instance.GetBirdForShooting();
    }

    //Rerolls the bird, should be called when reroll button is clicked
    private void RerollBird()
    {
        BirdBagManager.Instance.replaceBird(activeBird);
        activeBird = BirdBagManager.Instance.GetBirdForShooting();
        //Animatations
    }
}
