using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapManager : MonoBehaviour
{
    Scene currentScene;
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(currentScene.name);
    }
}
