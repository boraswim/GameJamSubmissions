using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : PlayerBaseState
{
    public JumpAction(PlayerMain player, PlayerStateMachine stateMachine, PlayerData.ModeAction modeAction) : base(player, stateMachine, modeAction)
    {
        jump = playerData.Actions.Jump;
    }

    PlayerData.ActionVariables.JumpVariables jump;

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.Rigidbody.velocity = new Vector3(player.Rigidbody.velocity.x, 0, player.Rigidbody.velocity.z);

        player.Rigidbody.drag = jump.Drag;
        player.Rigidbody.AddForce(player.transform.up * jump.JumpForce, ForceMode.Impulse);

        jump.JumpBufferTimer = 0;
        jump.CoyoteTimeTimer = 0;
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

        if (player.Rigidbody.velocity.y <= 0 && localTime > 0.1f)
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

        player.Rigidbody.AddForce(_moveDir * jump.AirSpeed * 10f, ForceMode.Force);
    }

    private void CheckVelocity()
    {
        Vector3 _horizontalSpeed = player.Rigidbody.velocity;
        _horizontalSpeed.y = 0f;
        if (_horizontalSpeed.magnitude > jump.MaxAirSpeed)
        {
            player.Rigidbody.velocity = new (_horizontalSpeed.normalized.x * jump.MaxAirSpeed, player.Rigidbody.velocity.y, _horizontalSpeed.normalized.z * jump.MaxAirSpeed);
        }
    }
}
