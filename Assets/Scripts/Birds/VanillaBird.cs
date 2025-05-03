using UnityEngine;

class VanillaBird : Bird
{
    public AudioClip captainPlainClip;
    public AudioSource vanillaAudioSource;
    //Weird ass Necessary Inheritance stuff
    [SerializeField] private float _speedModifier = 1.0f;
    public override float speedModifier
    {
        get { return _speedModifier; }
        set { _speedModifier = value; }
    }

    
    public override void ActivateAbility()
    {
        vanillaAudioSource.PlayOneShot(captainPlainClip);
    }
}
