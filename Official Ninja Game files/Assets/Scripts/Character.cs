using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    //protected Animator myAnimator;

    public Animator MyAnimator { get; private set; }

    //[SerializeField]
    //protected int health;

    

    [SerializeField]
    protected stat healthStat;


    

    [SerializeField]
    private EdgeCollider2D swordCollider;

    [SerializeField]
    private List<string> damageSources;

    public abstract bool IsDead { get; }


    [SerializeField]
    protected Transform knifePos;

    [SerializeField]
    protected float moveSpeed;

    protected bool facingRight;

    [SerializeField]
    private GameObject knifePrefab;

    public bool Attack { get; set; }

    public bool TakingDamage { get; set; }

    public EdgeCollider2D SwordCollider
    {
        get
        {
            return swordCollider;
        }

       
    }

    // Use this for initialization
    public virtual void Start () {
        facingRight = true;
        MyAnimator = GetComponent<Animator>();

        healthStat.Initialize();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract IEnumerator TakeDamage();

    public abstract void Death();

    public virtual void ChangeDirection()
    {
        facingRight = !facingRight;

        
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
                              


    }

    public virtual void ThrowKnife(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Knife>().Intialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Knife>().Intialize(Vector2.left);
        }
    }

    public void MeleeAttack()
    {
        swordCollider.enabled = true;
    }



    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }
    

    
}
