using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D platformCollider;

    [SerializeField]
    private BoxCollider2D platformTrigger;

    void Start()
    {
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("enemy"))
        {
            Physics2D.IgnoreCollision(platformCollider, other, true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("enemy"))
        {
            Physics2D.IgnoreCollision(platformCollider, other, false);
        }
    }
}
