using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    Scene currentScene;
    private Transform target;
    [SerializeField] float speed;
    public AudioSource robotWalk;
    public AudioSource robotDeath;

    public bool isAlive = true;
    public enemyBoundController Bounds;
    Transform lastPos;
    Animator robotAnim,deathAnim;
    public AudioSource deathAudio, failAudio;
    public GameObject player, otherplayer, canvas, fallManager;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void Start()
    {
        target = player.GetComponent<Transform>();
        robotAnim = GetComponent<Animator>();
        deathAnim = canvas.GetComponent<Animator>();
        robotWalk.Play();
    }

    private void Update()
    {
        
        if(isAlive && Bounds.chase)
        {
        UnityEngine.Vector2 followPos = new Vector2(target.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, followPos, speed * Time.deltaTime);
        if(transform.position.x < target.position.x)
            gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        else if(transform.position.x > target.position.x)
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine("deathSeq");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("test");
            isAlive = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            StartCoroutine("enemyDeath");
        }
    }
    private IEnumerator enemyDeath()
    {
        robotAnim.Play("enemyDeath");
        robotWalk.Stop();
        robotDeath.Play();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private IEnumerator deathSeq()
    {
    player.SetActive(false);
    otherplayer.SetActive(false);
    deathAnim.Play("deathAnim");
    deathAudio.Play();
    failAudio.Play();
    yield return new WaitForSeconds(3.0f);
    SceneManager.LoadScene(currentScene.name);
    }
}
