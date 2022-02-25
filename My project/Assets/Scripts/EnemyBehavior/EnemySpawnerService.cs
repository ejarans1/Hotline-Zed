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

    public float attackRate;
    public float nextAttack;
    public bool recentlyAttacked;

    // Start is called before the first frame update
    void Start()
    {
        attackRate = 10;
        nextAttack = 0;
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
    
        return Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        
    }

    private void determineRecentlyAttackReset()
    {
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            recentlyAttacked = false;
            Debug.Log("Reset Attack Timer");
        }
    }
}
