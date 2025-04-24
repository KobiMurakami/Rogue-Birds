using System;
using UnityEngine;

public class BlockHealth : MonoBehaviour
{
    [SerializeField] private float _damageThreshold = 0.2f;
    private float _currentHealth;
    public GameObject woodPrefab;
    public GameObject stonePrefab;
    private float woodHealth = 1f;
    private float stoneHealth = 3f;

    public delegate void BlockDestroyed(String type);
    public static event BlockDestroyed OnBlockDestroy;

    private void Awake()
    {
        if (woodPrefab)
        {
            _currentHealth = 1f;
        }
        
        if (stonePrefab)
        {
            _currentHealth = 3f;
        }
    }
        

    public void DamageEnemy(float damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0f)
        {
            Die();
            OnBlockDestroy?.Invoke("basic");
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
