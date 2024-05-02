using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent onEnter, onExit;
    void OnTriggerEnter(Collider other)
    {
        onEnter.Invoke();
        Debug.Log("Entered " + name);
    }
    void OnTriggerExit(Collider other)
    {
        onExit.Invoke();
        Debug.Log("Exited " + name);
    }
}
