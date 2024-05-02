using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTween_Hammer : DOTween_Animation
{
    public float duration, delay;
    public Ease ease;
    void Start()
    {
        if (playOnStart)
            transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0, 90, 90)), 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetDelay(1f)
            .SetEase(ease);
    }
}
