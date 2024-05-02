using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerMain PlayerMain { get; private set; }
    public PlayerBaseState CurrentState { get; private set; }

    public PlayerBaseState IdleAction { get; private set; }
    public PlayerBaseState WalkAction { get; private set; }
    public PlayerBaseState RunAction { get; private set; }
    public PlayerBaseState JumpAction { get; private set; }
    public PlayerBaseState LandAction { get; private set; }
    public PlayerBaseState DashAction { get; private set; }
    public PlayerBaseState GrapplingHookAction { get; private set; }

    public PlayerStateMachine(PlayerMain playerMain)
    {
        PlayerMain = playerMain;

        IdleAction = new IdleAction(PlayerMain, this, PlayerData.ModeAction.Idle);
        RunAction = new RunAction(PlayerMain, this, PlayerData.ModeAction.Run);
        WalkAction = new WalkAction(PlayerMain, this, PlayerData.ModeAction.Walk);
        JumpAction = new JumpAction(PlayerMain, this, PlayerData.ModeAction.Jump);
        LandAction = new LandAction(PlayerMain, this, PlayerData.ModeAction.Land);
        DashAction = new DashAction(PlayerMain, this, PlayerData.ModeAction.Dash);
        GrapplingHookAction = new GrapplingHookAction(PlayerMain, this, PlayerData.ModeAction.GrapplingHook);

        ChangeState(IdleAction);
    }

    public void ChangeState(PlayerBaseState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
