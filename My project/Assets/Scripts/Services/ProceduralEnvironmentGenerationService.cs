using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    GameObject proceduralController;

    GameObject proceduralPrefab;

    bool generateNewPlatFormFlag = false;


        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (generateNewPlatFormFlag){
            getSpawnPositions();
            getPreFabToUse();
            generatePreFabAtSpawnPosition();
            rollPlatformUpIntoView();
        }
        
    }

    private void getSpawnPositions(){
        GameObject spawnLocationParent = proceduralPrefab.transform.Find("SpawnLocations").gameObject;
        List<Transform> spawnLocations = new List<Transform>();
        for (int i = 0; i < spawnLocationParent.transform.childCount; i++){
            spawnLocations.Add(spawnLocationParent.transform.GetChild(0));
        }
    }

    private void getPreFabToUse(){
       GameObject preFabsParent = proceduralPrefab.transform.Find("PreFabs").gameObject;
        List<GameObject> prefabPositions = new List<GameObject>();
        for (int i = 0; i < preFabsParent.transform.childCount; i++){
            prefabPositions.Add(preFabsParent.transform.GetChild(0).gameObject);
        } 
    }
}
