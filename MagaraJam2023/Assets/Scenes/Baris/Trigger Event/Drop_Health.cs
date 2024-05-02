using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Health : Drop
{
    public float hp;
    public override void Take()
    {
        PlayerStats.instance.playerHealth.AddHP(hp);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HP Drop")
            Take();
    }
}
