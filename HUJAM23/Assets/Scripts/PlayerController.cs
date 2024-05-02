using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float jumpForce = 5.0f;

    bool grounded = true;
    public static bool walking = false;
    public static int facingRight = 1;
    Rigidbody2D playerRb;
    SpriteRenderer playerSr;
    Transform playerTr;
    Animator playerAnim;
    AudioSource jumpAudio;
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSr = GetComponent<SpriteRenderer>();
        playerTr = GetComponent<Transform>();
        playerAnim = GetComponent<Animator>();
        jumpAudio = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerRb.velocity.y);
        playerAnim.SetFloat("speed",MathF.Abs(playerRb.velocity.x));

        if(playerRb.velocity.x < -0.01f)
        {
            facingRight = -1;
            playerTr.localScale = new Vector3(-MathF.Abs(playerTr.localScale.x),playerTr.localScale.y,playerTr.localScale.z);
        }
        else if(playerRb.velocity.x > 0.01f)
        {
            facingRight = 1;
            playerTr.localScale = new Vector3(MathF.Abs(playerTr.localScale.x),playerTr.localScale.y,playerTr.localScale.z);
        }

        if(playerRb.velocity.x != 0 && grounded)
            walking = true;
        else
            walking = false;


        if(grounded && Input.GetKey(KeyCode.Space))
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            jumpAudio.Play();
            grounded = false;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground"))
            grounded = true;
    }
}
