using System;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 3f;
    [SerializeField] private float _damageThreshold = 0.2f;
    private float _currentHealth;
    private Animator animator;
    private bool isDying = false;

    public delegate void EnemyDied(String type);
    public static event EnemyDied OnEnemyDeath;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        animator = GetComponent<Animator>();
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
