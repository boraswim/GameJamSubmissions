using UnityEngine;

public class PlayerBaseState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerMain player;
    protected PlayerData playerData;
    protected PlayerData.ModeAction modeAction;
    protected bool changeMode;
    protected float localTime;

    public PlayerBaseState(PlayerMain player, PlayerStateMachine stateMachine, PlayerData.ModeAction modeAction)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.modeAction = modeAction;
        playerData = player.PlayerData;
    }

    public virtual void Enter()
    {
        DoChecks();

        playerData.CurrentAction = modeAction;
        player.AnimatorTPS.SetBool(modeAction.ToString(), true);
        player.AnimatorFPS.SetBool(modeAction.ToString(), true);
        localTime = 0f;
    }

    public virtual void Exit()
    {
        player.AnimatorTPS.SetBool(modeAction.ToString(), false);
        player.AnimatorFPS.SetBool(modeAction.ToString(), false);
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
        localTime += Time.deltaTime;
        DoChecks();
    }
    public virtual void DoChecks()
    {
        EssentialPhysics.SetFacingDirection(player.AnimatorFPS.transform, player.AnimatorTPS.transform, Camera.main.transform.forward);
        EssentialPhysics.GroundCheck(player.transform, playerData);
        EssentialPhysics.MovingPlatformCheck(player.Rigidbody, player.transform, playerData);
        EssentialPhysics.UpdateTimersAndChecks(playerData);

        EssentialPhysics.UpdateSecondaryActions(playerData);
        EssentialPhysics.ApplySecondaryActions(playerData);
    }
}
