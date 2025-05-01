using UnityEngine;

public class Lightning : MonoBehaviour
{
    //TODO add line renderer 
    
    [Header("Explosion Settings")]
    public float explosionRadius = 3f;
    public float explosionForce = 500f;
    public LayerMask affectedLayers;
    
    private bool solarFlareActive = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (solarFlareActive)
        {
            //SolarFlare Modifier
            //Destroys all affected objects within radius
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);
            foreach (Collider2D col in colliders)
            {
                if (col.gameObject != gameObject)
                {
                    if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("LevelMats"))
                    {
                        Destroy(col.gameObject);
                    }
                }
            }
            //---------------------TODO create a cool visual flash---------------------------------------
            Destroy(gameObject); 
        }
        else
        {
            //Explodes in the general area, Regular Activation
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
    }

    // Debug visualization in editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void SetSolarFlareActive()
    {
        solarFlareActive = true;
    }
}
