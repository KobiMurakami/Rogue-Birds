using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public BossWeakPointController controller;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bird"))
        {
            Debug.Log(gameObject.name + " hit by Bird");

            if (controller != null)
            {
                controller.NotifyTargetDestroyed();
            }

            gameObject.SetActive(false); // or Destroy(gameObject);
        }
    }
}

