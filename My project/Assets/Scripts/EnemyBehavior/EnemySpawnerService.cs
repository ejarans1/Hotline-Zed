using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerService : MonoBehaviour
{

    public GameObject enemyPrefab;

    public Transform spawnPoint;

    public int maxEnemiesSpawned = 10;

    public int initialSpawnLimit = 0;

    public bool isActive = false;

    public float spawnRate;
    public float nextSpawn;
    public bool recentlyAttacked;

    // Start is called before the first frame update
    void Start()
    {
        spawnRate = 1;
        nextSpawn = 0;
        recentlyAttacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        determineRecentlyAttackReset();
        if (!recentlyAttacked)
        {
            if (isActive)
            {
                if (initialSpawnLimit <= maxEnemiesSpawned)
                {
                    spawnNewEnemy(spawnPoint);
                    initialSpawnLimit++;
                    recentlyAttacked = true;
                }
                else
                {
                    //Do Nothing For TimeBeing
                }
            }
        }

    }



    public void setActiveFlag(bool valueToSet){
        isActive = valueToSet;
    }

    private GameObject spawnNewEnemy(Transform spawnPoint) {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemy.active = true;
        return enemy;

    }

    private void determineRecentlyAttackReset()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            recentlyAttacked = false;
            Debug.Log("Reset Attack Timer");
        }
    }
}
