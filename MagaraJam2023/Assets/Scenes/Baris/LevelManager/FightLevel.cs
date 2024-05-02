using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightLevel : LevelRoom
{
    public EnemySpawner enemySpawner;
    public bool failed = false;
    public override bool IsFailed()
    {
        return failed;
    }
    public override bool isFinished()
    {
        return enemySpawner.killedEnemies.Count >= 20;
    }
}
