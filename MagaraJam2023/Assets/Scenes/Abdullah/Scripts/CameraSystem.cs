using Cinemachine;
using DG.Tweening;
using System;
using UnityEngine;

public static class CameraSystem
{
    public static event Action<CinemachineVirtualCamera> OnCameraChanged;
    public static CinemachineVirtualCamera MainVirtualCamera;

    public static void ChangeCamera(CinemachineVirtualCamera newCam)
    {
        CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
        CinemachineVirtualCamera liveCamera = brain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();

        liveCamera.Priority = 0;
        newCam.Priority = 20;
        OnCameraChanged?.Invoke(newCam);
    }

    public static void DoFOV(float plusFOV, float duration)
    {
        CinemachineVirtualCamera cam = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();

        if (cam != null)
        {
            float originalFOV = cam.m_Lens.FieldOfView;
            float targetFOV = originalFOV + plusFOV;

            // Animate to target FOV and back using DOTween's Yoyo loop type
            DOTween.To(() => cam.m_Lens.FieldOfView, x => cam.m_Lens.FieldOfView = x, targetFOV, duration)
                   .SetLoops(2, LoopType.Yoyo);
        }
    }
}
