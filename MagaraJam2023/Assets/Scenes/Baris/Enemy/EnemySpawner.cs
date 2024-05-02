using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemyPrefabs = new List<Enemy>();
    [HideInInspector] public List<EnemyObject> spawnedEnemies = new List<EnemyObject>();
    [HideInInspector] public List<EnemyObject> killedEnemies = new List<EnemyObject>();
    public List<EnemyType> typesCanSpawn = new List<EnemyType>();
    public float interval;
    public Transform target;
    public List<Transform> spawnPoints = new List<Transform>();
    public void StartSpawn()
    {
        StartCoroutine("Spawn");
    }
    public void StopSpawn()
    {
        StopCoroutine("Spawn");
    }
    public void ResetEnemies()
    {
        StopCoroutine("Spawn");
        foreach (EnemyObject enemy in spawnedEnemies)
            enemy.gameObject.SetActive(false);
    }
    public IEnumerator Spawn()
    {
        yield return new WaitForSecondsRealtime(interval);
        EnemyType typeToSpawn = SelectType();
        Enemy enemyToSpawn = enemyPrefabs.Find(x => x.enemyType == typeToSpawn);
        int spawnedCount = spawnedEnemies.FindAll(x => x.enemyType == typeToSpawn).Count;
        int limit = enemyPrefabs.Find(x => x.enemyType == typeToSpawn).limitAtTheSameTime;
        if (spawnedCount < limit)
            SetEnemy(enemyToSpawn, typeToSpawn);
        yield return new WaitForSecondsRealtime(interval);
        StartCoroutine("Spawn");
    }
    public virtual void SetEnemy(Enemy enemyToSpawn, EnemyType typeToSpawn)
    {
        GameObject spawnedEnemy = Instantiate(enemyToSpawn.enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
        spawnedEnemy.GetComponentInChildren<EnemyFollowSystem>().target = target;
        spawnedEnemies.Add(new EnemyObject(spawnedEnemy, typeToSpawn));
        spawnedEnemy.GetComponentInChildren<EnemyHealthSystem>().OnDead.AddListener(() => killedEnemies.Add(new EnemyObject(spawnedEnemy, typeToSpawn)));
        if (typeToSpawn == EnemyType.Ranged)
            spawnedEnemy.GetComponentInChildren<EnemyAttackSystem>().attackRange += Random.Range(-1f, 3f);

    }
    public EnemyType SelectType()
    {
        List<EnemyType> typesCanSpawnLocal = new List<EnemyType>(typesCanSpawn);
        EnemyType currentSelected = EnemyType.Melee;
        for (int i = 0; i < typesCanSpawnLocal.Count; i++)
        {
            currentSelected = typesCanSpawnLocal[Random.Range(0, typesCanSpawnLocal.Count)];
            int spawnedCount = spawnedEnemies.FindAll(x => x.enemyType == currentSelected).Count;
            int limit = enemyPrefabs.Find(x => x.enemyType == currentSelected).limitAtTheSameTime;
            if (spawnedCount >= limit)
            {
                typesCanSpawnLocal.Remove(currentSelected);
                continue;
            }
            else
                break;
        }
        return currentSelected;
    }
}
public enum EnemyType
{
    Boss, Melee, Ranged
}
[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int limitAtTheSameTime;
    public EnemyType enemyType;
}
[System.Serializable]
public class EnemyObject
{
    public GameObject gameObject;
    public EnemyType enemyType;
    public EnemyObject(GameObject gameObject, EnemyType enemyType)
    {
        this.gameObject = gameObject;
        this.enemyType = enemyType;
    }

}