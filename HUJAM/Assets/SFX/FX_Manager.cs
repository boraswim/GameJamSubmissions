using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Manager : MonoBehaviour
{
    [Header("Sounds")]
    public AudioSource[] WalkSounds;
    public AudioSource[] JumpSounds;
    [Header("")]
    public ParticleSystem[] PS;
    private int flip = 0;
    private bool doLanding;
    [SerializeField] private LayerMask groundLayer;
    public Transform groundCheck;
    private int groundType;

    void WalkParticle()
    {
        PS[0].Play();
    }

    void OkeyToLandParticle()
    {
        doLanding = true;
    }

    void JumpParticle()
    {
        PS[1].Play();
    }

    void LandParticle()
    {
        PS[2].Play();
    }

    void PlayWalkSound()
    {
        WalkSounds[groundType + flip].Play();
        flip ^= 1;
    }

    void PlayJumpSound()
    {
        JumpSounds[0].Play();
    }

    void PlayLandSound()
    {
        JumpSounds[1].Play();
    }

    private void Update()
    {
        if (doLanding && Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer))
        {
            PlayLandSound();
            LandParticle();
            doLanding = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DifferentGround")
            groundType = 2;
        else
            groundType = 0;
    }
}
