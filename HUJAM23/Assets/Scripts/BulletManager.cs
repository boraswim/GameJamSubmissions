using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    Animator robotAnim;
    Collider2D robotCollider;
    GameObject collidedObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        collidedObject = other.gameObject;
        StartCoroutine("bulletDestroy");
    }

    private IEnumerator bulletDestroy(){
        if(!collidedObject.CompareTag("Bounds"))
            Destroy(gameObject);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
