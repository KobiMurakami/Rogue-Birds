using System;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private float damageThreshold = 0.5f;
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void DamageBlock(float damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0f)
        {
            Break();
        }
    }

    private void Break()
    {
        // Optional: add effects like particle system, sound
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactVelocity = collision.relativeVelocity.magnitude;

        if (impactVelocity > damageThreshold)
        {
            DamageBlock(impactVelocity);
        }
    }
}