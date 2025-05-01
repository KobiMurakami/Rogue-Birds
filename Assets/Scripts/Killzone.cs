using UnityEngine;

public class Killzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("KillZone touched: " + other.name);
                    Destroy(other.gameObject);
        }
        
    }
}
