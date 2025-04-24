using System.Collections;
using UnityEngine;

class LightningBird : Bird
{
    public GameObject lightning;
    public float lightingTimeDifference;
    
    float verticalOffset = 2f;
    
    
    //Weird ass Necessary Inheritance stuff
    [SerializeField] private float _speedModifier = 1.0f;
    public override float speedModifier
    {
        get { return _speedModifier; }
        set { _speedModifier = value; }
    }


    public override void ActivateAbility()
    {
        
        Vector2 lightningSpawn = new Vector2(transform.position.x, transform.position.y + verticalOffset);
        
        StartCoroutine(SpawnLightning(0));
        StartCoroutine(SpawnLightning(lightingTimeDifference));
        StartCoroutine(SpawnLightning(lightingTimeDifference * 2));
    }
    
    private IEnumerator SpawnLightning(float timeDifference)
    {
        yield return new WaitForSeconds(timeDifference);
        Vector2 lightningSpawn = new Vector2(transform.position.x, transform.position.y + verticalOffset);
        Instantiate(lightning, lightningSpawn, Quaternion.identity);
    }
}