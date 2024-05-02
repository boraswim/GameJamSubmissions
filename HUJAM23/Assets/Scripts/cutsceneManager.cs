using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutsceneManager : MonoBehaviour
{
    public AudioSource dialogue;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("cutscene");
    }

    private IEnumerator cutscene()
    {
        dialogue.Play();
        yield return new WaitForSeconds(8.0f);
        dialogue.Play();
        yield return new WaitForSeconds(6.0f);
        dialogue.Play();
        yield return new WaitForSeconds(6.0f);
        SceneManager.LoadScene(2);
    }
}
