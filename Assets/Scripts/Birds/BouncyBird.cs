using UnityEngine;

class BouncyBird : Bird
{
    
    //Weird ass Necessary Inheritance stuff
    [SerializeField] private float _speedModifier = 1.0f;
    public AudioClip bounceAbilitySound;
    public float bounceAbilityVolume = 1;
    public AudioClip bounceSound;
    public float bounceVolume = 1;
    public int maxBounceNoises;
    
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
        birdAudioSource.clip = bounceAbilitySound;
        birdAudioSource.volume = bounceAbilityVolume;
        birdAudioSource.pitch = 1f;
        birdAudioSource.Play();
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;             // Remove all momentum
            rb.angularVelocity = 0f;                // Stop any rotation
            rb.gravityScale = 1f;                   // Ensure gravity is on
            rb.isKinematic = false;                 // Just in case it was kinematic
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (maxBounceNoises > 0)
        {
            birdAudioSource.clip = bounceSound;
            birdAudioSource.pitch = 1f;
            birdAudioSource.PlayOneShot(bounceSound, bounceVolume);
            maxBounceNoises--;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            OnBouncyBirdKill?.Invoke();
            Destroy(gameObject);
        }
    }
    
    //TODO Create on collisions specifically with enemies to destroy it, also make event so that perk can know
}
