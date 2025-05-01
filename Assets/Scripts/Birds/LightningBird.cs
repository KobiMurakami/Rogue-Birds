using System.Collections;
using UnityEngine;

class LightningBird : Bird
{
    [Header("Sound")]
    public AudioSource lightningBirdChannel;
    public AudioClip lightningBirdSound;
    public float lightningBirdVolume;
    public float lightningBirdPitch;
    
    [Header("Lightning")]
    public GameObject lightning;
    public float lightingTimeDifference;
    
    float verticalOffset = 2f;
    
    //Events 
    
    public delegate void LightningSpawned(GameObject lightning);
    public static event LightningSpawned OnLightningSpawned;
    
    
    //Weird ass Necessary Inheritance stuff
    [SerializeField] private float _speedModifier = 1.0f;
    public override float speedModifier
    {
        get { return _speedModifier; }
        set { _speedModifier = value; }
    }


    public override void ActivateAbility()
    {
        //Sounds
        lightningBirdChannel.clip = lightningBirdSound;
        lightningBirdChannel.volume = lightningBirdVolume;
        lightningBirdChannel.pitch = lightningBirdPitch;
        lightningBirdChannel.Play();
        
        StartCoroutine(SpawnLightning(0));
        StartCoroutine(SpawnLightning(lightingTimeDifference));
        StartCoroutine(SpawnLightning(lightingTimeDifference * 2));
    }
    
    private IEnumerator SpawnLightning(float timeDifference)
    {
        yield return new WaitForSeconds(timeDifference);
        Vector2 lightningSpawn = new Vector2(transform.position.x, transform.position.y + verticalOffset);
        GameObject newLightning = Instantiate(lightning, lightningSpawn, Quaternion.identity);
        OnLightningSpawned?.Invoke(newLightning);
    }
}