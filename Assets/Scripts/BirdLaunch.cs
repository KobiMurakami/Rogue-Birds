using UnityEngine;

public class BirdLaunch : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;

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
    public void LaunchBird(Vector2 direction, float force)
    {
        rb.isKinematic = false;
        circleCollider.enabled = true;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
