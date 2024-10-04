using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    float randx;
    Vector2 whereToSpawn;
    public float spawnrate = 3f;
    float nextSpawn = 0.0f;



    public int enemynum = 0;

    // Use this for initialization
    void Start () {
        
		
	}
    
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Time.time > nextSpawn && enemynum <= 5)
        {
            nextSpawn = Time.time + spawnrate;
            randx = Random.Range(21.5f, 23.01f);
            whereToSpawn = new Vector2(randx, transform.position.y);
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
            enemynum = enemynum + 1;

        }


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);


        }
        if (other.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);


        }

    }
}
