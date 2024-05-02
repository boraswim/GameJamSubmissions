using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Run : PlayerState
{
    protected Vector2 input;
    protected float enterTime;
    public Run(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.RB.velocity.x < 0)
            player.transform.localScale = new Vector3(-1, 1, 1);
        else
            player.transform.localScale = new Vector3(1, 1, 1);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.JumpState);
        }
        if ((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            stateMachine.ChangeState(player.IdleState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.RB.velocity = new Vector2(playerData.idle_Xvelocity * Input.GetAxis("Horizontal"), player.RB.velocity.y);
    }
}
