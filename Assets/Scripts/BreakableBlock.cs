using System;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private float damageThreshold = 0.5f;
    private float _currentHealth;
    public enum BlockType { Glass, Wood, Stone, Steel }
    [SerializeField] private BlockType blockType;
    [SerializeField] private ParticleSystem breakEffect;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;
    private bool hasBroken = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        switch (blockType)
        {
            case BlockType.Glass:
                maxHealth = 2f;
                damageThreshold = 0.3f;
                break;
            case BlockType.Wood:
                maxHealth = 5f;
                damageThreshold = 0.5f;
                break;
            case BlockType.Stone:
                maxHealth = 10f;
                damageThreshold = 0.8f;
                break;
            case BlockType.Steel:
                maxHealth = Mathf.Infinity;
                damageThreshold = Mathf.Infinity;
                break;
        }

        _currentHealth = maxHealth;
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            switch (blockType)
            {
                case BlockType.Glass:
                    rb.mass = 1f;
                    rb.linearDamping = 0.5f;
                    rb.angularDamping = 0.5f;
                    break;
                case BlockType.Wood:
                    rb.mass = 2f;
                    rb.linearDamping = 0.7f;
                    rb.angularDamping = 0.7f;
                    break;
                case BlockType.Stone:
                    rb.mass = 5f;
                    rb.linearDamping = 1f;
                    rb.angularDamping = 1f;
                    break;
                case BlockType.Steel:
                    rb.mass = 20f; // much heavier
                    rb.linearDamping = 2f;
                    rb.angularDamping = 2f;
                    break;
            }
        }

    }

    public void DamageBlock(float damageAmount)
    {
        if (hasBroken) return;
        
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0f)
        {
            Break();
        }
    }

    private void Break()
    {
        if(hasBroken) return;
        hasBroken = true;
        
        if (breakEffect != null)
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        
        if (audioSource && breakSound != null)
        {
            GameObject tempAudio = new GameObject("breakSound");
            AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
            tempSource.clip = breakSound;
            tempSource.Play();
            Destroy(tempAudio, breakSound.length);
            //audioSource.PlayOneShot(breakSound);
        }
        MeshRenderer meshrender = GetComponent<MeshRenderer>();
        if(meshrender) meshrender.enabled = false;
        Collider col = GetComponent<Collider>();
        if(col) col.enabled = false;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactVelocity = collision.relativeVelocity.magnitude;

        if (impactVelocity > damageThreshold)
        {
            if (blockType == BlockType.Steel)
            {
                if (hitSound != null && audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(hitSound);
                }
            }
            else
            {
                DamageBlock(impactVelocity);
            }
            
        }
    }
}