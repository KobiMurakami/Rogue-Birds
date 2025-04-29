using UnityEngine;

class BouncyBird : Bird
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
        
        //specific ability logic
    }
    
    //TODO Create on collsions specifically with enemies to destoy it, also make event so that perk can know
}
