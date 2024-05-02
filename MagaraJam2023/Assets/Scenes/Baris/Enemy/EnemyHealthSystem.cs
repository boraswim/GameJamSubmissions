using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthSystem : MonoBehaviour
{
    public float hp;
    public UnityEvent OnDead, OnDamage;
    public void AttackToEnemy(float damage)
    {
        hp -= damage;
        if (hp <= 0)
            OnDead.Invoke();
        else
            OnDamage.Invoke();
    }
}
