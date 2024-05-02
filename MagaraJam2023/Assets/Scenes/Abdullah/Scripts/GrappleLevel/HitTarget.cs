using System;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
    public event Action OnHitBySpear;
    public bool hit;

    private void Start()
    {
        hit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 15)
        {
            hit = true;
            OnHitBySpear?.Invoke();
        }
    }
}
