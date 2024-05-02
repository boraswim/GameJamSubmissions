using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTween_Door : DOTween_Animation
{
    public BoxCollider bc;
    public void Close()
    {
        bc.enabled = false;
        gameObject.transform.DOMoveY(transform.position.y - 10, 2f);
    }
}
