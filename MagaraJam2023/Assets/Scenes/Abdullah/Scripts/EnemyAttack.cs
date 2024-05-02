using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float AttackDamage;

    public static event Action<float> OnPlayerHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            OnPlayerHit?.Invoke(AttackDamage);
        }
    }
}
