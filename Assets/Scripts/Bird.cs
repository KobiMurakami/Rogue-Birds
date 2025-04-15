using UnityEngine;

public abstract class Bird : MonoBehaviour
{
    public string birdName;
    public float mass;
    public float speed;
    
    public abstract void ActivateAbility();
    
}

//Austin's Bird
class LightningBird : Bird
{
    public override void ActivateAbility()
    {
        //Make cool lighting
        //specific ability logic
    }
}

//Add more birds
class BlueBird : Bird
{
    public override void ActivateAbility()
    {
        // BlueBird specific ability logic
    }
}