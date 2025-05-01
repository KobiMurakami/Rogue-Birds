using UnityEngine;

class BouncyBird : Bird
{
    
    //Weird ass Necessary Inheritance stuff
    [SerializeField] private float _speedModifier = 1.0f;
    
    //Events
    public delegate void BouncyBirdKill();
    public static event BouncyBirdKill OnBouncyBirdKill;
    public override float speedModifier
    {
        get { return _speedModifier; }
        set { _speedModifier = value; }
    }

    
    public override void ActivateAbility()
    {
        
        //specific ability logic
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            OnBouncyBirdKill?.Invoke();
            Destroy(gameObject);
        }
    }
    
    //TODO Create on collisions specifically with enemies to destroy it, also make event so that perk can know
}
