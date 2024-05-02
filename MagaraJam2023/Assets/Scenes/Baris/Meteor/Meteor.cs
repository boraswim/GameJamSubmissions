using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Meteor : MonoBehaviour
{
    public MeshRenderer mesh;
    public UnityEvent onAnimationFinish;
    public void PlayMeteorAnimation(UnityEvent onfinish, float speed, Vector3 targetPoint)
    {
        transform.DOMove(targetPoint, 20 / speed)
           .SetEase(Ease.InSine)
           .OnComplete(() =>
           {
               onfinish.Invoke();
               onAnimationFinish.Invoke();
               Destroy(mesh.gameObject);
           });
    }
}
