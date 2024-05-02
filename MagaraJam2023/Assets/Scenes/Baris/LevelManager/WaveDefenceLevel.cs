using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDefenceLevel : LevelRoom
{
    public float shrineHealth = 100;
    public EnemySpawner enemySpawner;
    public static WaveDefenceLevel instance;
    void Awake()
    {
        Singleton();
    }
    public void Singleton()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public override bool IsFailed()
    {
        return shrineHealth <= 0;
    }

    public override bool isFinished()
    {
        return enemySpawner.killedEnemies.Count >= 20;
    }
}
