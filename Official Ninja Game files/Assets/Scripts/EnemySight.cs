using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.Target = other.gameObject;
        }

        if (other.CompareTag("Knife"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
        }
        if (other.CompareTag("EnemyKnife"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.Target = null;
        }
    }
}
