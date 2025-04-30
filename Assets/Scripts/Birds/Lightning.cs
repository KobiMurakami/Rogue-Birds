using UnityEngine;

public class Lightning : MonoBehaviour
{
    //TODO add line renderer 
    
    [Header("Explosion Settings")]
    public float explosionRadius = 3f;
    public float explosionForce = 500f;
    public LayerMask affectedLayers;

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        //Explodes in the general area
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);
        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rb = col.attachedRigidbody;
            if (rb != null)
            {
                Vector2 direction = (rb.position - (Vector2)transform.position).normalized;
                rb.AddForce(direction * explosionForce);
            }
        }
        Destroy(gameObject);
    }

    // Debug visualization in editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
