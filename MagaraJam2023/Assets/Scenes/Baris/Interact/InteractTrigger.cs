using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class InteractTrigger : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color greenColor, redColor, whiteColor;
    public GameObject interactUI;
    public UnityEvent onInteract;
    public bool interacted = false;
    public MeshRenderer mesh;
    private Vector3 savedPos;
    void Awake()
    {
        savedPos = interactUI.transform.localPosition;
    }
    void OnTriggerEnter(Collider other)
    {
        interactUI.transform.DOScale(Vector3.one, 1f);
        DOVirtual.Vector3(savedPos - Vector3.up * 2, savedPos, 1f, (x) =>
        {
            interactUI.transform.localPosition = x;
        });
    }
    void OnTriggerStay(Collider other)
    {
        if (interacted)
        {
            interactUI.SetActive(false);
            return;
        }
        else
        {
            if (Input.GetKey(KeyCode.E))
            {
                onInteract.Invoke();
                interacted = true;
            }
        }
    }
    public void ResetInteract()
    {
        interacted = false;
        LightWhite();
    }
    public void LightGreen()
    {
        mesh.material.color = greenColor;
    }
    public void LightRed()
    {
        mesh.material.color = redColor;
    }
    public void LightWhite()
    {
        mesh.material.color = whiteColor;
    }
    void OnTriggerExit(Collider other)
    {
        DOTween.Kill(interactUI.transform);
        interactUI.transform.DOScale(Vector3.zero, 0f);
    }
}
