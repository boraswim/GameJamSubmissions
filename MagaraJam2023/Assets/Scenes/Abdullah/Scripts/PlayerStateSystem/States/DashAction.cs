using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAction : PlayerBaseState
{

    public DashAction(PlayerMain player, PlayerStateMachine stateMachine, PlayerData.ModeAction modeAction) : base(player, stateMachine, modeAction)
    {
        dash = playerData.Actions.Dash;
    }

    private PlayerData.ActionVariables.DashVariables dash;

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        ApplyDashForce();

        player.Rigidbody.useGravity = false;

        CameraSystem.DoFOV(dash.dashPlusFOV, dash.dashFOVDuration);

        playerData.Actions.Dash.dashCooldownTimer = player.PlayerData.Actions.Dash.dashCooldown;
        playerData.Physics.canDash = false;
    }

    public override void Exit()
    {
        base.Exit();
        player.Rigidbody.useGravity = true;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (localTime > dash.DashTime)
        {
            if (!playerData.Physics.isGrounded)
            {
                stateMachine.ChangeState(stateMachine.LandAction);
            }
            else
            {
                if (player.InputSystem.Input_Walk != Vector2.zero)
                {
                    if (player.InputSystem.Input_Run)
                    {
                        stateMachine.ChangeState(stateMachine.RunAction);
                    }
                    else
                    {
                        stateMachine.ChangeState(stateMachine.WalkAction);
                    }
                }
                else
                {
                    stateMachine.ChangeState(stateMachine.IdleAction);
                }
            }
        }
    }

    private void ApplyDashForce()
    {
        Vector2 _input = player.InputSystem.Input_Walk;
        if (_input == Vector2.zero)
        {
            Vector3 _forward = new(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
            Vector3 _up = new(0f, Camera.main.transform.forward.y, 0f);
            Vector3 _force = _forward * dash.DashForce + _up * dash.DashUpwardForce;

            player.Rigidbody.AddForce(_force, ForceMode.Impulse);
        }
        else
        {
            Vector3 _forward = new(_input.x, 0f, _input.y);

            _forward = Camera.main.transform.rotation * _forward;

            Vector3 _up = new(0f, 1f, 0f);
            
            Vector3 _force = _forward * dash.DashForce + _up * dash.DashUpwardForce;
            player.Rigidbody.AddForce(_force, ForceMode.Impulse);
        }
    }
}
