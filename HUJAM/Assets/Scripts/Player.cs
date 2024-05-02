using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    public Animator Anim;
    public Rigidbody2D RB;
    public Transform GroundCheck;
    public LayerMask GroundLayer;
    public bool manualMode;
    
    public PlayerData playerData;

    public Jump JumpState;
    public Idle IdleState;
    public Run RunState;
    public Death DeathState;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        JumpState = new Jump(this, StateMachine, playerData, "jump");
        IdleState = new Idle(this, StateMachine, playerData, "idle");
        RunState = new Run(this, StateMachine, playerData, "run");
        DeathState = new Death(this, StateMachine, playerData, "death");
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void RotatePlayer()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }
}
