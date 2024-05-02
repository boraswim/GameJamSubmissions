using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpear : MonoBehaviour
{
    public float Force, Drag;
    private Rigidbody rb;
    bool stopForce;
    private float localTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = Drag;
        stopForce = false;
        localTime = 0f;
    }

    private void Update()
    {
        localTime += Time.deltaTime;
        if (!stopForce || localTime < 0.5f)
        {
            rb.AddForce(Force * transform.up);
        }
        else
        {
            rb.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        stopForce = true;
    }
}
