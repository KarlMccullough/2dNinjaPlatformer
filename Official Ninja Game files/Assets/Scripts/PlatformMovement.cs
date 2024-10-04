using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

    private Vector3 posA;

    private Vector3 posB;

    [SerializeField]
    private float speed;

    private Vector3 nextPos;

    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;
    


	// Use this for initialization
	void Start ()
    {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;


	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
	}

    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed * Time.deltaTime);

        if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.04)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nextPos = nextPos != posA ? posA : posB;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") //|| Player.Instance.IsFalling)
        {
            //Player.Instance.MyAnimator.SetBool("land", false);

            if (Input.GetAxis("Horizontal") == Player.Instance.MyAnimator.GetFloat("speed"))
            {
                Player.Instance.MyAnimator.SetBool("land", false);
            }
            


            
            other.gameObject.layer = 10;
            
            other.transform.SetParent(childTransform);
            
            

        }
        
        /*if (Player.Instance.Jump)
        {
            
            other.transform.SetParent(null);
        }*/
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Player.Instance.Jump = false;
        other.transform.SetParent(null);
    }
}
