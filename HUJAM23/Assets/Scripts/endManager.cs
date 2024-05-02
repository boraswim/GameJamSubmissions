using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endManager : MonoBehaviour
{
    public GameObject Canvas;
    Animator endAnim;
    void Start()
    {
        endAnim = Canvas.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine("endGame");
        }
    }

    private IEnumerator endGame(){
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2.0f);
        endAnim.Play("endAnim");
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(0);

    }
}
