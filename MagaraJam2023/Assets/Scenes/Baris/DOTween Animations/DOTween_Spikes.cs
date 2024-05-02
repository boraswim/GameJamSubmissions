using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTween_Spikes : DOTween_Animation
{
    public void PlaySpike()
    {
        transform.DOLocalMoveY(2, 1f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.Linear);
    }
}
