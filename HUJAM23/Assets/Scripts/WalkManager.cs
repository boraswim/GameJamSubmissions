using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkManager : MonoBehaviour
{
    [SerializeField] float walkFreq;
    [SerializeField] AudioSource step1;

    // Start is called before the first frame update

    void OnEnable()
    {
        StartCoroutine("walkCoroutine");    
    }

    IEnumerator walkCoroutine(){
        yield return new WaitUntil(() => PlayerController.walking);
        step1.Play();
        yield return new WaitForSeconds(walkFreq);
        StartCoroutine("walkCoroutine");
    }   
}
