using System;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 3f;
    [SerializeField] private float _damageThreshold = 0.2f;
    private float _currentHealth;

    public delegate void EnemyDied(String type);
    public static event EnemyDied OnEnemyDeath;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void DamageEnemy(float damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0f)
        {
            Die();
            OnEnemyDeath?.Invoke("basic");
        }
    }

    private void Die()
    {
        Destroy(gameObject);
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
