using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System;

public delegate void DeadEventHandler();

public class Player : Character
{
    private PlayerStats thePlayerStats;



    [SerializeField]
    public Text levelText; //private

    public string LevelNumber;

    private static Player instance;

    public AudioManager audioManager;

   // [SerializeField]
    //private stat healthStat;

    public event DeadEventHandler Dead;


    [SerializeField]
    GameObject DeathUI;

    
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    private SpriteRenderer spriteRenderer;


    private bool immortal = false;

    [SerializeField]
    private float immortalTime;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatisGround;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    

    private float ScreenWidth;

    Vector3 localScale;

    public Rigidbody2D Rb { get; set; }

    public bool canMove;

    public bool Slide { get; set; }

    public bool Jump { get; set; }

    public bool OnGround { get; set; }

    public override bool IsDead
    {
        get
        {
            if (healthStat.CurrentVal <= 0)
            {
                OnDead();
            }

            return healthStat.CurrentVal <= 0;
        }
    }

    public bool IsFalling
    {
        get
        {
            return Rb.velocity.y < 0;
        }
    }

    private Vector2 StartPos;


    // Use this for initialization
    public override void Start ()
    {
       

        thePlayerStats = FindObjectOfType<PlayerStats>();

        audioManager = AudioManager.instance;


        base.Start();
        StartPos = transform.position;
        localScale = transform.localScale;
        ScreenWidth = Screen.width;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Rb = GetComponent<Rigidbody2D>();
       
        
        //healthStat.Initialize();

    }


   /* void Update()
    {
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -9f)
            {
                audioManager.PlaySound("Died");
                healthStat.CurrentVal -= 100;


                Death();
                //Rb.velocity = Vector2.zero;
                //transform.position = StartPos;
            }

           // HandleInput();

        }
      
       
    }*/


    void FixedUpdate ()   
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            
            levelText.text = "Lvl: " + thePlayerStats.currentLevel;

        }



        if (!canMove)
        {
            return;
        }

        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            horizontal = CrossPlatformInputManager.GetAxis("Horizontal");  //Comment these out if you want to move using keyboard

            float vertical = Input.GetAxis("Vertical");
            vertical = CrossPlatformInputManager.GetAxis("Vertical");  //Comment these out if you want to move using keyboard
            
            if (transform.position.y <= -15f)
            {
                audioManager.PlaySound("Died");
                healthStat.CurrentVal -= 100;


                Death();


                
                //Rb.velocity = Vector2.zero;
                //transform.position = StartPos;
            }

            HandleInput();

            OnGround = IsGrounded();

            HandleMovement(horizontal, vertical);  

            Flip(horizontal);
            
           
            HandleLayers();
        }
       

   
	}

    public void OnDead()
    {
        if (Dead != null)
        {
            Dead();
        }
    }
    private void HandleLayers()
    {
        if (!OnGround) //OnGround
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }



    private void HandleMovement(float horizontal, float vertical)   
    {

        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (IsFalling)
        {
            
            gameObject.layer = 11;
            MyAnimator.SetBool("land", true);
            
            //Jump = false;


        }
        if (!Attack && !Slide) //|| OnGround || airControl)
        {
           // audioManager.PlaySound("Slide Sound");
            Rb.velocity = new Vector2(horizontal * moveSpeed, Rb.velocity.y);
           


        }
        if (Jump && Rb.velocity.y == 0)
        {
            audioManager.PlaySound("Jump Sound");
            Rb.AddForce(new Vector2(0, jumpForce));
            


        }
        
        





    }


 


    
    private void HandleInput()    
    {
      
        if (Input.GetKeyDown(KeyCode.Space) && !IsFalling || CrossPlatformInputManager.GetButtonDown("jump") && !IsFalling)
        {
            MyAnimator.SetTrigger("jump");
          
            //Jump = true;
           
        }
        
        if (Input.GetKeyDown(KeyCode.P) || CrossPlatformInputManager.GetButtonDown("attack"))
        {
            MyAnimator.SetTrigger("attack");
          
        }
        
       
        if (Input.GetKeyDown(KeyCode.S) || CrossPlatformInputManager.GetButtonDown("slide"))
        {
            MyAnimator.SetTrigger("slide");
            

        }
        if(Input.GetKeyDown(KeyCode.O) || CrossPlatformInputManager.GetButtonDown("throw"))
        {
            MyAnimator.SetTrigger("throw");
            //audioManager.PlaySound("Knife Sound");


        }
       
    }
    private void Flip(float horizontal)    
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }

    }

   
    private bool IsGrounded()
    {
        if (Rb.velocity.y <= 0) //Rb.velocity.y <= 0
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatisGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }

                }
            }
        }
        return false;
    }
  
    public override void ThrowKnife(int value)
    {
        if (!OnGround && value == 1 || OnGround && value == 0)
        {
            audioManager.PlaySound("Knife Sound");
            base.ThrowKnife(value);

        }
    }

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;

            yield return new WaitForSeconds(.1f);

            spriteRenderer.enabled = true;

            yield return new WaitForSeconds(.1f);

        }
    }

    /*public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Water" && Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("jump") && col.gameObject.tag == "Water" && IsFalling )
        {
            MyAnimator.SetLayerWeight(1, 0);
            MyAnimator.SetTrigger("jump");
            //MyAnimator.SetTrigger("jump");
            //Jump = true;
        }
    }*/

    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
           
            healthStat.CurrentVal -= 10;

            if (!IsDead)
            {
                if (healthStat.CurrentVal >= 10)
                {
                    audioManager.PlaySound("Damaged");
                }
                
                MyAnimator.SetTrigger("damage");
                immortal = true;
                StartCoroutine(IndicateImmortal());

                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }
            else
            {
                audioManager.PlaySound("Died");
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
                Death();
            }
        }
    }

    public override void Death() 
    {
        
        Rb.velocity = Vector2.zero;
        //MyAnimator.SetTrigger("idle");
        DeathUI.gameObject.SetActive(true);

       
        //healthStat.CurrentVal = healthStat.MaxVal;
        //transform.position = StartPos;
    }

    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
    {
        float timer = 0;

        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            Rb.velocity = new Vector2(0, 0);
            Rb.AddForce(new Vector3(knockbackDir.x * - 10, knockbackDir.y * knockbackPwr, transform.position.z));
        }

        yield return 0;
    }
    /*public override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Spikes")
        {
           
          
            healthStat.CurrentVal -= 10;

            StartCoroutine(Knockback(1f, 10, transform.position));

        }

    }*/

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            GameManager1.Instance.CollectedCoins++;
            Destroy(other.gameObject);

        }
        if (other.gameObject.tag == "Spikes")
        {
            //StartCoroutine(Knockback(3f, 500, transform.position));
            healthStat.CurrentVal -= 10;

            
            
        }
        if (other.gameObject.tag == "blade")
        {
            //StartCoroutine(Knockback(3f, 500, transform.position));
            healthStat.CurrentVal -= 20;



        }
        if (other.gameObject.tag == "thorns")
        {
            //StartCoroutine(Knockback(3f, 500, transform.position));
            healthStat.CurrentVal -= 10;



        }

    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Spikes")
        {
          
            healthStat.CurrentVal -= 5;

        }

    }
}
                    
                   
          
