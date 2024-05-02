using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookAction : PlayerBaseState
{
    public GrapplingHookAction(PlayerMain player, PlayerStateMachine stateMachine, PlayerData.ModeAction modeAction) : base(player, stateMachine, modeAction)
    {
        grapplingHook = player.PlayerData.Actions.GrapplingHook;
    }

    PlayerData.ActionVariables.GrapplingHookVariables grapplingHook;

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        grapplingHook.grapplingGun.StartGrapple();
    }

    public override void Exit()
    {
        base.Exit();
        grapplingHook.grapplingGun.StopGrapple();
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

        if (!player.InputSystem.Input_GrapplingHook)
        {
            stateMachine.ChangeState(stateMachine.LandAction);
        }
    }

    private void MovePlayer()
    {
        Vector2 _walkInput = player.InputSystem.Input_Walk;
        Vector3 _moveDir = (Camera.main.transform.forward * _walkInput.y) + (Camera.main.transform.right * _walkInput.x);
        _moveDir.y = 0;
        _moveDir.Normalize();
        player.Rigidbody.AddForce(grapplingHook.HorizontalSpeed * _moveDir * 10f, ForceMode.Force);
    }

    private void CheckVelocity()
    {
        if (player.Rigidbody.velocity.magnitude > grapplingHook.HorizontalMaxSpeed)
        {
            player.Rigidbody.velocity = player.Rigidbody.velocity.normalized * grapplingHook.HorizontalMaxSpeed;
        }
    }
}
