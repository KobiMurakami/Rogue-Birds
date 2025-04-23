using UnityEngine;

class VanillaBird : Bird
{
    public override void ActivateAbility()
    {
        Destroy(gameObject);
        //Make cool lighting
        //specific ability logic
    }
}
