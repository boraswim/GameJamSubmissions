using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public GameObject CamFuture, CamNow;
    public GameObject PlayerFuture, PlayerNow;
    public GameObject Rain;
    public GameObject Canvas;
    public AudioSource todayAmb, futureAmb;
    Animator transitionAnim;
    AudioSource transitionAudio;
    Transform TrFuture, TrNow;
    Rigidbody2D RbFuture, RbNow;
    bool transition;

    // Start is called before the first frame update

    void Awake()
    {
        TrFuture = PlayerFuture.GetComponent<Transform>();
        TrNow = PlayerNow.GetComponent<Transform>();
        RbFuture = PlayerFuture.GetComponent<Rigidbody2D>();
        RbNow = PlayerNow.GetComponent<Rigidbody2D>();
        transitionAnim = Canvas.GetComponent<Animator>();
        transitionAudio = GetComponent<AudioSource>();        
    }
    void Start()
    {
        StartCoroutine("Teleport");
    }

    // Update is called once per frame

                private IEnumerator Teleport()
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q));
                transitionAudio.Play();
                if(PlayerFuture.activeInHierarchy)
            {
                futureAmb.Stop();
                transitionAnim.Play("transitionAnim");
                RbFuture.gravityScale = 0f;
                PlayerFuture.SetActive(false);
                CamFuture.SetActive(false);
                TrNow.position = new Vector3(TrFuture.position.x + 250,TrFuture.position.y,TrFuture.position.z);
                CamNow.SetActive(true);
                PlayerNow.SetActive(true);
                RbNow.gravityScale = 1.75f;
                Rain.SetActive(true);
                todayAmb.Play();
            }
            else if(PlayerNow.activeInHierarchy)
            {
                todayAmb.Stop();
                Rain.SetActive(false);
                transitionAnim.Play("transitionAnim");
                RbNow.gravityScale = 0f;
                PlayerNow.SetActive(false);
                CamNow.SetActive(false);
                TrFuture.position = new Vector3(TrNow.position.x - 250,TrNow.position.y,TrNow.position.z);
                CamFuture.SetActive(true);
                PlayerFuture.SetActive(true);
                RbFuture.gravityScale = 1.75f;
                futureAmb.Play();
            }
            yield return new WaitForSeconds(1.0f);
            StartCoroutine("Teleport");
            }
}
