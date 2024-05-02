using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : PlayerBaseState
{
    public IdleAction(PlayerMain player, PlayerStateMachine stateMachine, PlayerData.ModeAction modeAction) : base(player, stateMachine, modeAction)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.Rigidbody.drag = playerData.Actions.Idle.Drag;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.Rigidbody.velocity = Vector3.zero;
    }

    public override void Update()
    {
        base.Update();
        if (player.InputSystem.Input_Walk != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.WalkAction);
        }
        else if (player.InputSystem.Input_Jump && playerData.Physics.canJump)
        {
            stateMachine.ChangeState(stateMachine.JumpAction);
        }
        else if (player.InputSystem.Input_Dash && playerData.Physics.canDash)
        {
            stateMachine.ChangeState(stateMachine.DashAction);
        }
        else if (!playerData.Physics.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.LandAction);
        }
        else if (player.InputSystem.Input_GrapplingHook)
        {
            stateMachine.ChangeState(stateMachine.GrapplingHookAction);
        }
    }
}
