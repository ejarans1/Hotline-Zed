using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform spawnPoint1;

    public Transform spawnpPoint2;

    public Transform spawnPoint3;

    public Transform spawnPoint4;

    List<Transform> spawnPointList;
    void Start()
    {
        spawnPointList = new List<Transform>();
        spawnPointList.Add(spawnPoint1);
        spawnPointList.Add(spawnpPoint2);
        spawnPointList.Add(spawnPoint3);
        spawnPointList.Add(spawnPoint4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform returnRandomSpawnPoint(){
        int num = Random.Range(0,4); 
        return spawnPointList[num];
    }
}
