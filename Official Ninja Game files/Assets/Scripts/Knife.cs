using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Knife : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody2D rb;

    private Vector2 direction;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        
	}

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

   public void Intialize(Vector2 direction)
    {
        this.direction = direction;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
