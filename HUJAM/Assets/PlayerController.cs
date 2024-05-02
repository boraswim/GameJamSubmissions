using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum State
    {
        STATE_IDLE,
        STATE_RUN,
        STATE_JUMP,
        STATE_ARROW,
        STATE_DEATH
    };

    State state = State.STATE_IDLE;
    private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    private bool arrowReturnPressed;
    public GameObject[] lights;

    public GameObject TheBlueThingy;

    /*[HideInInspector]*/ public bool isControllingArrow = false;
    /*[HideInInspector]*/ public bool arrowReturn = false;
    //Animations
    private Animator animator;
    private string currentState;
    const string idle = "PlayerIdle";
    const string run = "PlayerRun";
    const string jump = "PlayerJump";
    const string death = "PlayerDeath";
    const string idleArrow = "PlayerIdleArrow";
    bool facingRight = true;
    float unplannedAttackEndTime = 0.0f;
    public float unplannedAttackCooldown = 0.0f;

    private GameObject arrow;
    private Arrow arrowScript;
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        arrow = GameObject.FindGameObjectWithTag("Arrow");
        arrowScript = arrow.GetComponent<Arrow>();
    }
    void Start()
    {
    }


    void Update()
    {
        switch (state)
        {
            case State.STATE_IDLE:
                if((Input.GetAxisRaw("Horizontal") == 0))
                    ChangeAnimationState(idle);
                Idle();
                break;
            case State.STATE_RUN:
                if (isGrounded)
                    ChangeAnimationState(run);
                else
                    ChangeAnimationState(jump);
                Run();
                break;
            case State.STATE_JUMP:
                ChangeAnimationState(jump);
                Jump();
                break;
            case State.STATE_ARROW:
                ChangeAnimationState(idleArrow);
                lights[0].SetActive(false);
                lights[1].SetActive(true);
                Arrow();
                break;
            case State.STATE_DEATH:
                ChangeAnimationState(death);
                this.enabled = false;
                break;
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        checkScale();
       if(Input.GetKeyDown(KeyCode.R) && arrowScript.isStuck)
        {
            arrowScript.isStuck = false;
            arrowReturnPressed = true;
            state = State.STATE_ARROW;
        }
    }
    private void checkScale()
    {
        if ((Input.GetAxisRaw("Horizontal") == -1 && facingRight) || (!facingRight && Input.GetAxisRaw("Horizontal") == 1))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingRight = !facingRight;
        }

    }
    public void deathState()
    {
        state = State.STATE_DEATH;

    }
    private void Arrow()
    {
        if(arrowScript.isStuck)
        {
            lights[0].SetActive(true);
            lights[1].SetActive(false);
            state = State.STATE_IDLE;
        }
        if (Input.GetKeyDown(KeyCode.R) || (Time.time >= unplannedAttackEndTime && isControllingArrow && !arrowScript.isStuck) || arrowReturnPressed)
        {
            arrowReturnPressed = false;
            arrowScript.isStuck = false;
            arrow.GetComponent<Collider2D>().isTrigger = true;
            isControllingArrow = false;
            arrowReturn = true;
            arrow.GetComponent<Arrow>().enabled = true;

            unplannedAttackCooldown = Time.time + 2.0f;

            state = State.STATE_IDLE;
            lights[0].SetActive(true);
            lights[1].SetActive(false);
        }
        /*if(!isControllingArrow && !arrowReturn)
        {
        }*/
    }
    private void Run()
    {
        if(Time.time >= unplannedAttackCooldown)
            TheBlueThingy.SetActive(true);
        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb2d.velocity.y);

        if(Input.GetKeyDown(KeyCode.E) && isGrounded && Time.time >= unplannedAttackCooldown)
        {
            TheBlueThingy.SetActive(false);
            unplannedAttackEndTime = Time.time + 4.0f;
            state = State.STATE_ARROW;
            isControllingArrow = true;
            
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            state = State.STATE_JUMP;
        }
        else if(Input.GetAxisRaw("Horizontal") == 0 && isGrounded && rb2d.velocity.y==0)
            state = State.STATE_IDLE;

    }
    private void Jump()
    {
       
        if (Input.GetAxisRaw("Horizontal") != 0) //input al�yorsa
        {
            state = State.STATE_RUN;
        }
        else if (rb2d.velocity.y == 0)
        {
            state = State.STATE_IDLE;
        }

    }
    private void Idle()
    {
        if(Time.time >= unplannedAttackCooldown)
            TheBlueThingy.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E) && isGrounded && Time.time >= unplannedAttackCooldown)
        {
            TheBlueThingy.SetActive(false);
            unplannedAttackEndTime = Time.time + 4.0f;
            state = State.STATE_ARROW;
            isControllingArrow = true;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            state = State.STATE_JUMP;
        }
        else if (Input.GetAxisRaw("Horizontal") != 0)
            state = State.STATE_RUN;
    }
  
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
