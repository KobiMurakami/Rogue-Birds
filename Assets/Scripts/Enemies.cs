using System;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 3f;
    [SerializeField] private float _damageThreshold = 0.2f;
    [SerializeField] private AudioClip deathSound;
    private float _currentHealth;
    private Animator animator;
    private bool isDying = false;
    private AudioSource audioSource;

    public delegate void EnemyDied(String type);
    public static event EnemyDied OnEnemyDeath;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void DamageEnemy(float damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0f)
        {
            Die();
            
        }
    }

    private void Die()
    {
        if (isDying) return;
        isDying = true;

        animator.SetTrigger("Die");
        OnEnemyDeath?.Invoke("basic");

        if (deathSound != null)
        {
            GameObject tempAudio = new GameObject("EnemyDeathSound");
            AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
            tempSource.clip = deathSound;
            tempSource.Play();
            Destroy(tempAudio, deathSound.length);
        }
        //GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactVelocity = collision.relativeVelocity.magnitude;

        if (impactVelocity > _damageThreshold)
        {
            DamageEnemy(impactVelocity);
        }
    }
}
