using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [Serializable]
    public class PhysicsVariables
    {
        public LayerMask groundLayerMask;
        public LayerMask movingPlatformLayerMask;
        public LayerMask enemyLayerMask;
        public bool isOnMovablePlatform;
        public Vector3 grondCheckPosition;
        public bool isGrounded;
        public bool canDash;
        public bool canJump;

        public LayerMask grappableLayerMask;
        public bool isGrappable;

        public bool readyToThrowSpear;

        [NonEditable] public Vector2 facingDirection;
    }
    [Serializable]
    public class ActionVariables
    {
        [Serializable]
        public class IdleVariables
        {
            public float Drag;
        }
        [Serializable]
        public class WalkVariables
        {
            public float MaxSpeed;
            public float Speed;
            public float Drag;
        }
        [Serializable]
        public class RunVariables
        {
            public float MaxSpeed;
            public float Speed;
            public float Drag;
        }
        [Serializable]
        public class JumpVariables
        {
            public float JumpForce;
            public float AirSpeed;
            public float MaxAirSpeed;
            public float Drag;

            public float CoyoteTimeMaxTime;
            [NonEditable] public float CoyoteTimeTimer;
            public float JumpBufferMaxTime;
            [NonEditable] public float JumpBufferTimer;
        }
        [Serializable]
        public class LandVariables
        {
            public float AirSpeed;
            public float MaxAirSpeed;
            public float Drag;
        }
        [Serializable]
        public class DashVariables
        {
            public float DashForce;
            public float DashUpwardForce;
            public float DashTime;
            public float dashCooldown;
            [NonEditable] public float dashCooldownTimer;
            
            public float dashPlusFOV;
            public float dashFOVDuration;
        }
        [Serializable]
        public class GrapplingHookVariables
        {
            public GrapplingGun grapplingGun;
            public GrapplingRope grapplingRope;
            public RotateGun rotateGun;

            public float HorizontalMaxSpeed;
            public float HorizontalSpeed;
            public float Drag;
        }
        [Serializable]
        public class AttackVariables
        {
            public Animator spearAnimator;

            public float spearAimMaxTime;
            public float spearAimTimer;

            public Transform spear;
            public GameObject spearPrefab;

            public float ThrowForce;
            public float ThrowDrag;

            public float GetSpearMaxTime;
            public float GetSpearTimer;
        }

        public IdleVariables Idle;
        public WalkVariables Walk;
        public RunVariables Run;
        public JumpVariables Jump;
        public LandVariables Land;
        public DashVariables Dash;
        public GrapplingHookVariables GrapplingHook;
        public AttackVariables Spear;
    }

    public enum ModeAction
    {
        Idle,
        Walk,
        Run,
        Jump,
        Land,
        Dash,
        GrapplingHook,
    }

    public enum SpearAction
    {
        Idle,
        Run,
        Attack1,
        Attack2,
        Aim,
        GetSpear,
        ThrowSpear,
    }

    [NonEditable] public ModeAction CurrentAction;
    public SpearAction secondaryAction;

    public PhysicsVariables Physics;
    public ActionVariables Actions;
}
