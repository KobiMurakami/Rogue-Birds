using System.Collections;
using UnityEngine;

class BlowfishBird : Bird
{
    
    [Header ("Audio")]
    public AudioClip expandSound;
    public float expandSoundVolume = 2f;
    
    public AudioClip explodeSound;
    public float explodeSoundVolume = 2f;
    
    [Header("Blowfish Variables")]
    public float shrinkDelay = 8f;
    public GameObject needlePrefab; 
    public int numberOfNeedles = 12;
    public float shootForce = 5f;
    
    
    //Weird ass Necessary Inheritance stuff
    [SerializeField] private float _speedModifier = 1.0f;
    public override float speedModifier
    {
        get { return _speedModifier; }
        set { _speedModifier = value; }
    }
    
    //Events
    public delegate void BlowjayAbility(BlowfishBird bird);
    public static event BlowjayAbility OnBlowjayAbillity;
    
    //Expand the bird and shoot out needles
    public override void ActivateAbility()
    {
        birdAudioSource.clip = expandSound;
        birdAudioSource.pitch = 2.0f; //Speed modifier custom to blowfish clip
        birdAudioSource.volume = expandSoundVolume;
        birdAudioSource.Play();
        OnBlowjayAbillity?.Invoke(this);
        gameObject.GetComponent<Animator>().SetTrigger("Expand");
        StartCoroutine(SpawnNeedles());
        StartCoroutine(ShrinkBlowfish());
    }

    private IEnumerator ShrinkBlowfish()
    {
        yield return new WaitForSeconds(shrinkDelay);
        
        gameObject.GetComponent<Animator>().SetTrigger("Contract");
    }

    private IEnumerator SpawnNeedles()
    {
        yield return new WaitForSeconds(.66f); //Hard coded based on animation
        
        birdAudioSource.clip = explodeSound;
        birdAudioSource.pitch = 1.0f;
        birdAudioSource.PlayOneShot(explodeSound, explodeSoundVolume);
        float angleStep = 360f / numberOfNeedles;
        float angle = 0f;

        for (int i = 0; i < numberOfNeedles; i++)
        {
            // Calculate direction
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 direction = new Vector2(dirX, dirY).normalized;

            // Calculate angle of rotation 
            float needleAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.Euler(0, 0, needleAngle);
            
            // Create needle
            Vector2 spawnPos = (Vector2)transform.position + direction * 1.8f; //Hard coded radius
            GameObject needle = Instantiate(needlePrefab, spawnPos, rotation);
            Rigidbody2D rb = needle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                //Calculation and force taking momentum into account
                Rigidbody2D blowfishRb = GetComponent<Rigidbody2D>();
                Vector2 inheritedVelocity = blowfishRb != null ? blowfishRb.linearVelocity : Vector2.zero;
                rb.linearVelocity  = direction * shootForce + inheritedVelocity;
            }

            angle += angleStep;
        }
    }
}
