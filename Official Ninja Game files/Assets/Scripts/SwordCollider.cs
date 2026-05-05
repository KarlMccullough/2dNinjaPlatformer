using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    [SerializeField]
    private string targetTag;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
