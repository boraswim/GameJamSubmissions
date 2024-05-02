using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTween_FireTrap : DOTween_Animation
{
    public BoxCollider bc;
    public ParticleSystem pc;
    public int particleEmitCount;
    public float fireTime = 1f;
    public float fireDelay = 1f;
    private void Start()
    {
        StartCoroutine("ApplyFire");
    }
    public IEnumerator ApplyFire()
    {
        pc.Emit(particleEmitCount);
        bc.enabled = true;
        yield return new WaitForSeconds(fireTime);
        bc.enabled = false;
        yield return new WaitForSeconds(fireDelay);
        StartCoroutine("ApplyFire");
    }

}
