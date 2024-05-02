using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandAction : PlayerBaseState
{
    public LandAction(PlayerMain player, PlayerStateMachine stateMachine, PlayerData.ModeAction modeAction) : base(player, stateMachine, modeAction)
    {
        land = playerData.Actions.Land;
    }

    PlayerData.ActionVariables.LandVariables land;

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.Rigidbody.drag = land.Drag;
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

        if (playerData.Physics.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.WalkAction);
        }
        else if (player.InputSystem.Input_Dash && playerData.Physics.canDash)
        {
            stateMachine.ChangeState(stateMachine.DashAction);
        }
        else if (playerData.Physics.canJump)
        {
            stateMachine.ChangeState(stateMachine.JumpAction);
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

        player.Rigidbody.AddForce(_moveDir * land.AirSpeed * 10f, ForceMode.Force);
    }

    private void CheckVelocity()
    {
        Vector3 _horizontalSpeed = player.Rigidbody.velocity;
        _horizontalSpeed.y = 0f;
        if (_horizontalSpeed.magnitude > land.MaxAirSpeed)
        {
            player.Rigidbody.velocity = new(_horizontalSpeed.normalized.x * land.MaxAirSpeed, player.Rigidbody.velocity.y, _horizontalSpeed.normalized.z * land.MaxAirSpeed);
        }
    }
}
