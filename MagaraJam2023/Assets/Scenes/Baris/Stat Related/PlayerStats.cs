using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Stats stats = new Stats();
    public PlayerHealthManager playerHealth;
    public static PlayerStats instance;
    public BuffInventory buffInventory;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void AddStats(Stats stats)
    {
        this.stats += stats;
    }
}
[System.Serializable]
public class Stats
{
    public float maxHP;
    public float movementSpeed;
    public float attackDamage;
    public float stamina;
    public float jumpForce;
    public int dashCount;

    public static Stats operator +(Stats a, Stats b)
    {
        Stats returnValue = new Stats()
        {
            maxHP = a.maxHP + b.maxHP,
            movementSpeed = a.movementSpeed + b.movementSpeed,
            attackDamage = a.attackDamage + b.attackDamage,
            dashCount = a.dashCount + b.dashCount,
            jumpForce = a.jumpForce + b.jumpForce,
            stamina = a.stamina + b.stamina,
        };
        return returnValue;
    }
    public static Stats operator *(Stats a, int b)
    {
        Stats returnValue = new Stats()
        {
            maxHP = a.maxHP * b,
            movementSpeed = a.movementSpeed * b,
            attackDamage = a.attackDamage * b,
            dashCount = a.dashCount * b,
            jumpForce = a.jumpForce * b,
            stamina = a.stamina * b,
        };
        return returnValue;
    }
}