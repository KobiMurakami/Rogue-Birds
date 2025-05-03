using UnityEngine;

public class BombBird : Bird
{
    [Header("Explosion Settings")]
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private GameObject explosionEffect;

    //private Rigidbody2D rb;

    //Weird ass Necessary Inheritance stuff
    [SerializeField] private float _speedModifier = 1.0f;

    public override float speedModifier
    {
        get { return _speedModifier; }
        set { _speedModifier = value; }
    }
    protected override void Awake()
    {
        base.Awake();
        //rb = GetComponent<Rigidbody2D>();
    }

    public override void ActivateAbility()
    {
        ApplyExplosionForce();
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void ApplyExplosionForce()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rbHit = hit.GetComponent<Rigidbody2D>();
            if (rbHit != null && rbHit != rb) // Exclude self
            {
                Vector2 direction = (Vector2)hit.transform.position - (Vector2)transform.position;
                float distance = direction.magnitude;

                if (distance < explosionRadius)
                {
                    float falloff = 1 - (distance / explosionRadius);
                    rbHit.AddForce(direction.normalized * explosionForce * falloff, 
                                   ForceMode2D.Impulse);
                }
            }
        }
    }

    // Visualize explosion radius in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
