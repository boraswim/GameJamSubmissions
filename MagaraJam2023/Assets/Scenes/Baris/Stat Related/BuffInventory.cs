using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffInventory : MonoBehaviour
{
    public List<Buff> buffDatas = new List<Buff>();
    public List<Debuff> debuffDatas = new List<Debuff>();
    public List<Buff> ownedBuffs = new List<Buff>();
    public List<Debuff> ownedDebuffs = new List<Debuff>();
    public bool CheckDebuff(DebuffType debuffType)
    {
        return ownedDebuffs.Exists(x => x.debuffType == debuffType);
    }
    public bool CheckBuff(BuffType buffType)
    {
        return ownedBuffs.Exists(x => x.buffType == buffType);
    }
    public void AddBuff(int buffInt)
    {
        Buff buff = buffDatas.Find(x => x.buffType == (BuffType)buffInt);
        ownedBuffs.Add(buff);
        buff.onAddStats.Invoke(buff.stats);
        buff.onAdd.Invoke();
    }
    public void AddDebuff(int DebuffInt)
    {
        Debuff debuff = debuffDatas.Find(x => x.debuffType == (DebuffType)DebuffInt);
        ownedDebuffs.Add(debuff);
        debuff.onAddStats.Invoke(debuff.stats);
        debuff.onAdd.Invoke();
    }
}
[System.Serializable]
public class Buff
{
    public BuffType buffType;
    public Stats stats = new Stats();
    public UnityEvent<Stats> onAddStats;
    public UnityEvent onAdd;
}
[System.Serializable]
public class Debuff
{
    public DebuffType debuffType;
    public Stats stats = new Stats();
    public UnityEvent<Stats> onAddStats;
    public UnityEvent onAdd;
}
public enum BuffType
{
    MovementSpeed, AttackDamage, HP, DashCount, JumpForce, Stamina, Rebirth
}
public enum DebuffType
{
    BrokenLeg, SpearAimRemoval, AttackAndHPReduction, StaminaAndMovementSpeedReduction
}