using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackDamage;

    public static event Action<GameObject, float> OnEnemyHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13)
        {
            OnEnemyHit?.Invoke(other.gameObject, AttackDamage);
        }
    }
}
