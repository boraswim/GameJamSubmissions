using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavmeshEnemySpawner : EnemySpawner
{
    public override void SetEnemy(Enemy enemyToSpawn, EnemyType typeToSpawn)
    {
        GameObject spawnedEnemy = Instantiate(enemyToSpawn.enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
        spawnedEnemy.GetComponentInChildren<NavmeshEnemySystem>().target = target;
        spawnedEnemies.Add(new EnemyObject(spawnedEnemy, typeToSpawn));
        spawnedEnemy.GetComponentInChildren<EnemyHealthSystem>().OnDead.AddListener(() => killedEnemies.Add(new EnemyObject(spawnedEnemy, typeToSpawn)));
    }
}
