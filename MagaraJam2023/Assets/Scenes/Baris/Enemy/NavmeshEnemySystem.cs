using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NavmeshEnemySystem : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target, enemyTransform;
    public UnityEvent onRange, afterAttack;
    public float attackRange, delayBetweenAttacks;
    void Start()
    {
        StartCoroutine("WaitForInRange");
    }
    void Update()
    {
        agent.destination = target.position;
    }
    public void SetAgentSpeed(float speed)
    {
        agent.speed = speed;
    }
    public IEnumerator WaitForInRange()
    {
        float speed = agent.speed;
        yield return new WaitUntil(() => Vector3.Distance(enemyTransform.position, new Vector3(target.position.x, enemyTransform.position.y, target.position.z)) <= attackRange);
        SetAgentSpeed(0);
        onRange.Invoke();
        yield return new WaitForSecondsRealtime(delayBetweenAttacks);
        SetAgentSpeed(speed);
        afterAttack.Invoke();
        StartCoroutine("WaitForInRange");
    }
}
