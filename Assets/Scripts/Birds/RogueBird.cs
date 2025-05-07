using UnityEngine;

class RogueBird : Bird
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
        Vector2 forward = transform.right; // Assuming the bird's forward direction is 'right'
        float forwardSpeed = Vector2.Dot(rb.linearVelocity, forward);
        rb.linearVelocity += forward * (2f * forwardSpeed); // Add 2x to make total 2x

        rb.mass *= 2f;
        
    }
}
