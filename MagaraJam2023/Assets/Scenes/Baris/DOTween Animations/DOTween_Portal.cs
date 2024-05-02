using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTween_Portal : MonoBehaviour
{
    public Ease ease;
    public float portalScale;
    public void OpenPortal()
    {
        Vector3 savedPos = transform.localPosition;
        transform.DOScale(new Vector3(0.15f * portalScale, 0.225f * portalScale, 0.15f * portalScale), 1f)
        .SetEase(ease);
        transform.DOLocalMoveY(savedPos.y + 0.5f, 1f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(ease);
    }
    public void ClosePortal()
    {
        transform.DOScale(Vector2.zero, 1f)
        .SetEase(ease);
    }
}
