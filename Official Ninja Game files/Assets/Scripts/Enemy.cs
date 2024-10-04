using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



public class Enemy : Character
{
    public GameObject bossPrefab;
    
    public float minsize;

   


    private PlayerStats thePlayerStats;

    public int expToGive;
    
    private static Enemy instance;

    private Player player;

    public static Enemy Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Enemy>();
            }
            return instance;
        }
    }


    AudioManager audioManager;

    public event DeadEventHandler Dead; ///////////////////

    private IEnemyState currentState;

    public GameObject Target { get; set; }

    [SerializeField]
    private Transform leftEdge;

    [SerializeField]
    private Transform rightEdge;

    [SerializeField]
    private float meleeRange;

    [SerializeField]
    private float throwRange;

    private Canvas healthCanvas;

    private bool dropItem = true;

    public bool canMove;

    public bool InMeleeRange
    {
        get
        {
            if(Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }

            return false;
        }
    }

    public bool InThrowRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;
            }

            return false;
        }
    }
    
    private Vector2 StartPos1;
    
   
    // Use this for initialization
    public override void Start ()
    {
        thePlayerStats = FindObjectOfType<PlayerStats>();
        player = FindObjectOfType<Player>();
        audioManager = AudioManager.instance;

        base.Start();
        StartPos1 = transform.position;
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
        ChangeState(new Idlestate());

        healthCanvas = transform.GetComponentInChildren<Canvas>();

        
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        // currentState.Execute();
        //LookAtTarget();
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                
                currentState.Execute();
                
                
            }      
            LookAtTarget();
            //currentState.Execute();
            
        }
       
       
	}
   

    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new PatrolState());
    }
    public void OnDead()
    {
        if (Dead != null)
        {
            Dead();
        }
        
    }

    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;

        currentState.Enter(this);

    }

    public void Move()
    {
        if (!Attack)
        {
            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                MyAnimator.SetFloat("speed", 1);

                transform.Translate(GetDirection() * (moveSpeed * Time.deltaTime));

            }
            else if (currentState is PatrolState)
            {

                ChangeDirection();
            }
            else if (currentState is RangedState)
            {
                Target = null;
                ChangeState(new Idlestate());
            }
            
        }

    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);


        }
        base.OnTriggerEnter2D(other);
        //currentState.OnTriggerEnter(other);

       
    }

    public override bool IsDead
    {
        
        get
        {
            
            return healthStat.CurrentVal <= 0;
        }
        
    }

   

    public override IEnumerator TakeDamage()
    {
        if (!healthCanvas.isActiveAndEnabled)
        {
            healthCanvas.enabled = true;

        }

        healthStat.CurrentVal -= 10;

        GetComponent<AudioSource>().Play();

        if (!IsDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            if (dropItem)
            {
                thePlayerStats.AddExperience(expToGive);
                

                GameObject coin = (GameObject)Instantiate(GameManager1.Instance.CoinPrefab, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity);

                Physics2D.IgnoreCollision(coin.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                dropItem = false;
                

            }
            /*if (healthStat.CurrentVal<=0 && transform.localScale.y > minsize) //transform.localScale.y > minsize
            {
                minsize = minsize + 1.25f;

                GameObject clone1 = Instantiate(bossPrefab, new Vector3(transform.position.x - 1, transform.position.y + 5, transform.position.z), transform.rotation) as GameObject;
                GameObject clone2 = Instantiate(bossPrefab, new Vector3(transform.position.x + 1, transform.position.y + 5, transform.position.z), transform.rotation) as GameObject;


                clone1.transform.localScale = new Vector3(transform.localScale.y, transform.localScale.y, transform.localScale.z);

                clone2.transform.localScale = new Vector3(transform.localScale.y, transform.localScale.y, transform.localScale.z);

               

            }*/



            //healthStat.XPCurrentVal += 20;
            MyAnimator.SetTrigger("die");



            yield return null;
        }
    }

    public override void ChangeDirection()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        Transform tmp = transform.FindChild("EnemyHealthBarCanvas").transform;
#pragma warning restore CS0618 // Type or member is obsolete

        Vector3 pos = tmp.position;

        tmp.SetParent(null);

        base.ChangeDirection();

        tmp.SetParent(transform);

        tmp.position = pos;
    }

    public override void Death()
    {
        dropItem = true;
        MyAnimator.ResetTrigger("die");
        //MyAnimator.SetTrigger("idle");
        //healthStat.CurrentVal = healthStat.MaxVal;


        
        Destroy(gameObject);
        
        //transform.position = StartPos1;
        healthCanvas.enabled = false;
    }
}
