using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

    [SerializeField]
    private Enemy enemy;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.Target = other.gameObject;
        }

        if (other.tag == "Knife")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
        }
        if (other.tag == "EnemyKnife")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.Target = null;
        }
    }
}
