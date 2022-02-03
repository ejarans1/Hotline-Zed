using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerService : MonoBehaviour
{

    public GameObject enemyPrefab;

    public Transform spawnPoint;

    public int maxEnemiesSpawned = 10;

    public int initialSpawnLimit = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (initialSpawnLimit <= maxEnemiesSpawned){
            spawnNewEnemy(spawnPoint);
            initialSpawnLimit++;
            
        }
        else {
            Destroy(this.gameObject);
        }

    }

    private GameObject spawnNewEnemy(Transform spawnPoint) {
    
        return Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        
    }
}
