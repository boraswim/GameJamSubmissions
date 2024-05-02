using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DoTween_Boss : MonoBehaviour
{
    public float yAxis, duration;
    public Ease ease;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(yAxis, duration).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
