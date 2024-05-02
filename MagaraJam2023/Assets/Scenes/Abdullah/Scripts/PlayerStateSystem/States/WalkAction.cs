using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAction : PlayerBaseState
{
    public WalkAction(PlayerMain player, PlayerStateMachine stateMachine, PlayerData.ModeAction modeAction) : base(player, stateMachine, modeAction)
    {
        walk = playerData.Actions.Walk;
    }

    private PlayerData.ActionVariables.WalkVariables walk;

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.Rigidbody.drag = walk.Drag;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        MovePlayer();
    }

    public override void Update()
    {
        base.Update();

        CheckVelocity();

        if (player.InputSystem.Input_Run && player.InputSystem.Input_Walk != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.RunAction);
        }
        else if (player.Rigidbody.velocity.magnitude <= 0.5f && player.InputSystem.Input_Walk == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdleAction);
        }
        else if (player.InputSystem.Input_Jump && playerData.Physics.canJump)
        {
            stateMachine.ChangeState(stateMachine.JumpAction);
        }
        else if (!playerData.Physics.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.LandAction);
        }
        else if (player.InputSystem.Input_Dash && playerData.Physics.canDash)
        {
            stateMachine.ChangeState(stateMachine.DashAction);
        }
        else if (player.InputSystem.Input_GrapplingHook)
        {
            stateMachine.ChangeState(stateMachine.GrapplingHookAction);
        }
    }

    private void MovePlayer()
    {
        Vector2 _walkInput = player.InputSystem.Input_Walk;
        Vector3 _moveDir = (Camera.main.transform.forward * _walkInput.y) + (Camera.main.transform.right * _walkInput.x);
        _moveDir.y = 0;
        _moveDir.Normalize();

        player.Rigidbody.AddForce(walk.Speed * _moveDir * 10f, ForceMode.Force);
    }

    private void CheckVelocity()
    {
        if (player.Rigidbody.velocity.magnitude > walk.MaxSpeed)
        {
            player.Rigidbody.velocity = player.Rigidbody.velocity.normalized * walk.MaxSpeed;
        }
    }
}
