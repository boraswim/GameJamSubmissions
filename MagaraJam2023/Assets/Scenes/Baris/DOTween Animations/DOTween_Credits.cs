using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTween_Credits : MonoBehaviour
{
    public RectTransform rect;
    public float duration;
    void OnEnable()
    {
        DOVirtual.Vector3(new Vector3(0, -2000, 0), new Vector3(0, 2000, 0), duration, x => { rect.anchoredPosition = x; });
    }
}
