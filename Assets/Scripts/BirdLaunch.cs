using UnityEngine;

public class BirdLaunch : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private bool hasBeenLaunched;
    private bool shouldFaceVelocityDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        rb.isKinematic = true;
        circleCollider.enabled = false;
    }

    private void FixedUpdate()
    {
        if(hasBeenLaunched && shouldFaceVelocityDirection)
        {
            transform.right = rb.linearVelocity;
        }
        
    }
    public void LaunchBird(Vector2 direction, float force)
    {
        rb.isKinematic = false;
        circleCollider.enabled = true;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        hasBeenLaunched = true;
        shouldFaceVelocityDirection = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        shouldFaceVelocityDirection = false;
    }
}
