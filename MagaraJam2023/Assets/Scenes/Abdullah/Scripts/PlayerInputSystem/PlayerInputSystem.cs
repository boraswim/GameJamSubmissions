using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    public PlayerInputActions playerControls;
    public PlayerMain player;

    public Vector2 Input_Walk;
    public bool Input_Run;
    public bool Input_Jump;
    public bool Input_Dash;
    public bool Input_GrapplingHook;
    public bool Input_Attack;
    public bool Input_Aim;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        player = GetComponent<PlayerMain>();
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
        
        playerControls.Player.Walk.performed += WalkPerformed;
        playerControls.Player.Walk.canceled += WalkPerformed;

        playerControls.Player.Run.performed += RunPerformed;
        playerControls.Player.Run.canceled += RunPerformed;
        
        playerControls.Player.Jump.started += JumpPerformed;
        playerControls.Player.Jump.performed += JumpPerformed;
        playerControls.Player.Jump.canceled += JumpPerformed;
        
        playerControls.Player.Dash.started += DashPerformed;
        playerControls.Player.Dash.performed += DashPerformed;
        playerControls.Player.Dash.canceled += DashPerformed;
        
        playerControls.Player.GrapplingHook.performed += GrapplingHookPerformed;
        playerControls.Player.GrapplingHook.canceled += GrapplingHookPerformed;
        
        playerControls.Player.Attack.started += AttackPerformed;
        
        playerControls.Player.Aim.performed += AimPerformed;
        playerControls.Player.Aim.canceled += AimPerformed;

    }

    private void OnDisable()
    {
        playerControls.Player.Walk.performed -= WalkPerformed;
        playerControls.Player.Walk.canceled -= WalkPerformed;

        playerControls.Player.Run.performed -= RunPerformed;
        playerControls.Player.Run.canceled -= RunPerformed;
        
        playerControls.Player.Jump.performed -= JumpPerformed;
        playerControls.Player.Jump.canceled -= JumpPerformed;
        
        playerControls.Player.Dash.started -= DashPerformed;
        playerControls.Player.Dash.performed -= DashPerformed;
        playerControls.Player.Dash.canceled -= DashPerformed;
        
        playerControls.Player.GrapplingHook.performed -= GrapplingHookPerformed;
        playerControls.Player.GrapplingHook.canceled -= GrapplingHookPerformed;
        
        playerControls.Player.Attack.started -= AttackPerformed;
        
        playerControls.Player.Aim.performed -= AimPerformed;
        playerControls.Player.Aim.canceled -= AimPerformed;
        
        playerControls.Player.Disable();
    }

    private void WalkPerformed(InputAction.CallbackContext context)
    {
        Input_Walk = context.ReadValue<Vector2>();
    }

    private void RunPerformed(InputAction.CallbackContext context)
    {
        Input_Run = context.ReadValueAsButton();
    }

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        Input_Jump = context.ReadValueAsButton();
        if (context.started)
        {
            player.PlayerData.Actions.Jump.JumpBufferTimer = player.PlayerData.Actions.Jump.JumpBufferMaxTime;
        }
    }

    private void DashPerformed(InputAction.CallbackContext context)
    {
        Input_Dash = context.ReadValueAsButton();
    }

    private void GrapplingHookPerformed(InputAction.CallbackContext context)
    {
        Input_GrapplingHook = context.ReadValueAsButton();
    }

    private void AttackPerformed(InputAction.CallbackContext context)
    {
        Input_Attack = context.ReadValueAsButton();

        if (player.PlayerData.secondaryAction == PlayerData.SpearAction.Idle || player.PlayerData.secondaryAction == PlayerData.SpearAction.Run)
        {
                player.PlayerData.secondaryAction = Random.Range(0f,1f) <= 0.5f ? PlayerData.SpearAction.Attack1 : PlayerData.SpearAction.Attack2;
        }
        else if (player.PlayerData.secondaryAction == PlayerData.SpearAction.Aim)
        {
            if (player.PlayerData.Physics.readyToThrowSpear)
            {
                player.PlayerData.secondaryAction = PlayerData.SpearAction.ThrowSpear;
                EssentialPhysics.ThrowSpearAction(player.PlayerData);
            }
        }
    }

    private void AimPerformed(InputAction.CallbackContext context)
    {
        Input_Aim = context.ReadValueAsButton();
        if (player.PlayerData.secondaryAction == PlayerData.SpearAction.Idle || player.PlayerData.secondaryAction == PlayerData.SpearAction.Run)
        {
            player.PlayerData.secondaryAction = PlayerData.SpearAction.Aim;
            player.PlayerData.Actions.Spear.spearAimTimer = player.PlayerData.Actions.Spear.spearAimMaxTime;
        }

        if (context.canceled)
        {
            player.PlayerData.Actions.Spear.spearAnimator.SetBool("Aim", false);
            if (player.PlayerData.secondaryAction == PlayerData.SpearAction.Aim)
            {
                if (player.PlayerData.CurrentAction == PlayerData.ModeAction.Run)
                {
                    player.PlayerData.secondaryAction = PlayerData.SpearAction.Run;
                    player.PlayerData.Actions.Spear.spearAnimator.SetBool("Run", true);
                    player.PlayerData.Actions.Spear.spearAnimator.SetBool("Idle", false);
                }
                else
                {
                    player.PlayerData.secondaryAction = PlayerData.SpearAction.Idle;
                    player.PlayerData.Actions.Spear.spearAnimator.SetBool("Run", false);
                    player.PlayerData.Actions.Spear.spearAnimator.SetBool("Idle", true);
                }
            }
        }
    }
}
