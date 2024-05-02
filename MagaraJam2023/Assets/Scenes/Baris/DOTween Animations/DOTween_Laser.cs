using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTween_Laser : DOTween_Animation
{
    public Vector3 startPoint, endPoint;
    public float duration;
    public Ease ease;
    private void Start()
    {
        if (playOnStart)
        {
            DOVirtual.Vector3(startPoint, endPoint, duration, x =>
            {
                transform.localPosition = x;
            })
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(ease);
        }
    }
}
