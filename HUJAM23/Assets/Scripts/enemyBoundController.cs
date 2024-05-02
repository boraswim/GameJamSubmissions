using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBoundController : MonoBehaviour
{
    public bool chase = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            chase = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            chase = false;
    }
}
