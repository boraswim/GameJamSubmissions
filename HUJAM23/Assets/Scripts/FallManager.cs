using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallManager : MonoBehaviour
{
    Scene currentScene;
    public GameObject Canvas, Player1, Player2;
    Animator deathAnim;
    public AudioSource deathAudio, failAudio;
    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        deathAnim = Canvas.GetComponent<Animator>();
    }
void OnTriggerEnter2D(Collider2D other)
{
    if(other.gameObject.CompareTag("Player"))
        StartCoroutine("fall");
}

private IEnumerator fall()
{
    Player1.SetActive(false);
    Player2.SetActive(false);
    deathAnim.Play("deathAnim");
    deathAudio.Play();
    failAudio.Play();
    yield return new WaitForSeconds(3.0f);
    SceneManager.LoadScene(currentScene.name);
}

}
