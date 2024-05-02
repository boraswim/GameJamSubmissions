using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthManager : MonoBehaviour
{
    public float health;
    public UnityEvent OnHPRemove;
    public UnityEvent OnHPAdd;
    public UnityEvent OnDeath;
    public UnityEvent OnRebirth;
    void Start()
    {
        health = PlayerStats.instance.stats.maxHP;
    }
    public void AddHP(float hp)
    {
        health += hp;
        if (health > PlayerStats.instance.stats.maxHP)
            health = PlayerStats.instance.stats.maxHP;
    }
    public void RemoveHP(float hp)
    {
        health -= hp;
        if (health <= 0)
        {
            if (!PlayerStats.instance.buffInventory.CheckBuff(BuffType.Rebirth))
                OnDeath.Invoke();
            else
            {
                OnRebirth.Invoke();
                health = PlayerStats.instance.stats.maxHP;
            }
        }
    }
}
