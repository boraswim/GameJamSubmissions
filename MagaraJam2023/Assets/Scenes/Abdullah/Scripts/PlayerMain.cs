using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : Singleton<PlayerMain>
{
    public Animator AnimatorFPS;
    public Animator AnimatorTPS;
    [NonEditable] public Rigidbody Rigidbody;
    [NonEditable] public PlayerInputSystem InputSystem;
    [NonEditable] public PlayerStateMachine StateMachine;
    public PlayerData PlayerData;

    public CinemachineVirtualCamera FPSCam;

    protected override void Awake()
    {
        base.Awake();
        Rigidbody = GetComponent<Rigidbody>();
        InputSystem = GetComponent<PlayerInputSystem>();
        StateMachine = new(this);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        CameraSystem.ChangeCamera(FPSCam);
        CameraSystem.MainVirtualCamera = FPSCam;
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CameraTrigger cameraTrigger))
        {
            CameraSystem.ChangeCamera(cameraTrigger.Camera);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<CameraTrigger>(out CameraTrigger cameraTrigger))
        {
            CameraSystem.ChangeCamera(CameraSystem.MainVirtualCamera);
        }
    }

}
