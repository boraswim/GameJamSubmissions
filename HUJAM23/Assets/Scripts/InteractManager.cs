using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    bool inside = false;
    public GameObject bridgeToOpen;
    Animator leverAnim;

    void Start()
    {
        leverAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(inside && Input.GetKeyDown(KeyCode.E))
        {
            bridgeToOpen.SetActive(true);
            Destroy(gameObject.transform.GetChild(0).gameObject);
            leverAnim.Play("leverPull");
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inside = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inside = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
