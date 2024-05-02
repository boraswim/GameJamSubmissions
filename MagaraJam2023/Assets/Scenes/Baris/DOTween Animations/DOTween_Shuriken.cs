using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTween_Shuriken : MonoBehaviour
{
    public Ease ease;
    public float duration;
    public Vector3 rotation;
    void Start()
    {
        Follow();
        Idle();
    }
    public void Idle()
    {
        transform.DOLocalRotate(rotation, duration)
        .SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
    public void Follow()
    {
        transform.DOLocalMoveY(transform.localPosition.y + 0.2f, duration)
           .SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }
}
