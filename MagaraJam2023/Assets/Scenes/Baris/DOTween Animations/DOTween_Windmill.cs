using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTween_Windmill : MonoBehaviour
{
    public float rotationSpeed;
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 45), rotationSpeed, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Incremental)
        .SetEase(Ease.Linear)
        .SetSpeedBased(true);
    }
}
