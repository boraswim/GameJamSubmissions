using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerState
{
    protected Vector2 input;
    protected float enterTime;
    public Idle(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enterTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0)
        {
            stateMachine.ChangeState(player.RunState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
