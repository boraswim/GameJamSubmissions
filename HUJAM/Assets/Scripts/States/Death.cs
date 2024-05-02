using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : PlayerState
{
    protected Vector2 input;
    protected float enterTime;
    public Death(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        Debug.Log("Time passed: " + (Time.time - enterTime).ToString());
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
