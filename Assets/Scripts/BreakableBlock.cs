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

    private void Awake()
    {
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
        if (breakEffect != null)
            Instantiate(breakEffect, transform.position, Quaternion.identity);
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