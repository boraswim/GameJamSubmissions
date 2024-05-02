using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowSystem : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public float velocityMultiplier;
    public float speedSetInterval;
    public float rotationOffset;
    void Start()
    {
        StartCoroutine("SetVelocityCoroutine");
    }
    public IEnumerator SetVelocityCoroutine()
    {
        yield return new WaitForSeconds(speedSetInterval);
        SetVelocity();
        StartCoroutine("SetVelocityCoroutine");
    }
    void Update()
    {
        float x = Mathf_Extra.GetAngleBetweenPoints(target.transform.position, transform.position);
        transform.localEulerAngles = Vector3.up * (x + rotationOffset);
    }
    public void SetVelocity()
    {
        Vector3 direction = (new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position).normalized;
        rb.velocity = direction * velocityMultiplier;
    }
    public void SetVelocityMultiplier(float newSpeed)
    {
        velocityMultiplier = newSpeed;
    }
}
