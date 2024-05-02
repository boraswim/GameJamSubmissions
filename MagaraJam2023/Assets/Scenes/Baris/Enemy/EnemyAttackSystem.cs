using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackSystem : MonoBehaviour
{
    public EnemyFollowSystem enemyFollowSystem;
    public Transform enemyTransform;
    public float attackRange;
    public UnityEvent OnAttack, onReachRange;
    public float attackValue;
    public float attackInterval;
    void Start()
    {
        StartCoroutine("WaitForAttackRange");
    }
    public IEnumerator WaitForAttackRange()
    {
        yield return new WaitUntil(() => Vector3.Distance(enemyTransform.position, new Vector3(enemyFollowSystem.target.position.x, enemyTransform.position.y, enemyFollowSystem.target.position.z)) <= attackRange);
        onReachRange.Invoke();
        StartCoroutine("AttackCoroutine");
    }
    public IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(attackInterval);
        OnAttack.Invoke();
        StartCoroutine("AttackCoroutine");
    }
    public void AttackShrine()
    {
        WaveDefenceLevel.instance.shrineHealth -= attackValue;
    }
}
