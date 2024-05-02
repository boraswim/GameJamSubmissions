using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    enum State
    {
        STATE_RUN,
        STATE_ATTACK,
        STATE_IDLE,
        STATE_DEATH
    };

    //Animations
    private Animator animator;
    private string currentState;
    const string idle = "slimeIdleAnim";
    const string run = "slimeRunAnim";
    const string attack = "slimeAttackAnim";
    const string death = "slimeDeathAnim";

    //Following & CoolDown
    private GameObject player;
    private Transform playerPos;
    private Vector2 currentPlayerPos;
    public float distance;
    public float speedEnemy = 5f;
    float timer;

    //Move
    Vector3 movement;
    bool Moveright = false;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }


    State state = State.STATE_IDLE;
    void Update()
    {
        checkState();
    }
    void checkState()
    {
        switch (state)
        {
            case State.STATE_RUN:
                following();
                ChangeAnimationState(run);
                break;
            case State.STATE_ATTACK:
                ChangeAnimationState(attack);
                player.GetComponent<PlayerController>().deathState();
                break;
            case State.STATE_IDLE:
                checkPlayer();
                ChangeAnimationState(idle);
                break;
            case State.STATE_DEATH:
                ChangeAnimationState(death);
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                GetComponent<Collider2D>().enabled = false;
                this.enabled = false;
                break;
        }
    }
    public void deathState()
    {
        state = State.STATE_DEATH;
    }

    void checkPlayer()
    {
        if (Vector2.Distance(transform.position, playerPos.position) < distance)
        {
            if (Vector2.Distance(transform.position, playerPos.position) <= 1)
                state = State.STATE_ATTACK;
            else
                state = State.STATE_RUN;
        }
        else
            state = State.STATE_IDLE;

    }
    void following()
    {
        flip();
        currentPlayerPos = new Vector2(playerPos.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, currentPlayerPos, speedEnemy * Time.deltaTime);
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.CompareTag("Player") && state == State.STATE_RUN)
        {
            Debug.Log("player");
            state = State.STATE_ATTACK;
        }
        if(trig.CompareTag("Arrow"))
        {
            Debug.Log("öldüm");
            state = State.STATE_DEATH;
        }
    }
    void flip()
    {
        if (playerPos.position.x > (transform.position.x + 0.5f))
        {
            if (!Moveright)
            {
                transform.Rotate(0f, 180f, 0f);
                Moveright = true;
            }
        }
        else
        {
            if (Moveright)
            {
                transform.Rotate(0f, 180f, 0f);
                Moveright = false;
            }
        }
    }
}
