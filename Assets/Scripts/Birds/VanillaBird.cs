using UnityEngine;

class VanillaBird : Bird
{
    
    //Weird ass Necessary Inheritance stuff
    [SerializeField] private float _speedModifier = 1.0f;
    public override float speedModifier
    {
        get { return _speedModifier; }
        set { _speedModifier = value; }
    }

    
    public override void ActivateAbility()
    {
        Destroy(gameObject);
        //Make cool lighting
        //specific ability logic
    }
}
