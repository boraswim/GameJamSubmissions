using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Normal Mode Variables")]
    public float idle_Xvelocity;
    public float defaultGravity;
    public float jump_Yforce;
    public float jump_upperGravity;
    public float jump_lowerGravity;

}

