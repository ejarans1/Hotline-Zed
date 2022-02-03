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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive){
            if (initialSpawnLimit <= maxEnemiesSpawned){
                    spawnNewEnemy(spawnPoint);
                    initialSpawnLimit++;
                }
            else {
                //Do Nothing For TimeBeing
            }
        }
        

    }

    public void setActiveFlag(bool valueToSet){
        isActive = valueToSet;
    }

    private GameObject spawnNewEnemy(Transform spawnPoint) {
    
        return Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        
    }
}
