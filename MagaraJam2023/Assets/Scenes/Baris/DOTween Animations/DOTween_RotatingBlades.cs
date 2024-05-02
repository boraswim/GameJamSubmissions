using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTween_RotatingBlades : DOTween_Animation
{
    public Vector3 rotateSpeed;
    public float duration;
    public Ease ease;
    void Start()
    {
        if (playOnStart)
        {
            transform.DOLocalRotate(rotateSpeed, duration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(ease);
        }
    }
}
