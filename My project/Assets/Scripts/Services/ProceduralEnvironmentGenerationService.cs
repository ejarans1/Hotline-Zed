using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralEnvironmentGenerationService : MonoBehaviour
{
    public GameObject proceduralController;

    public GameObject proceduralPrefab;

    public Transform environmentParent;

    bool generateNewPlatFormFlag = false;

    float speed;


        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (generateNewPlatFormFlag){
            Transform spawnPosition = getSpawnPositions();
            GameObject prefabToUse = proceduralPrefab;
            GameObject generatedPrefab = generatePreFabAtSpawnPosition(prefabToUse, spawnPosition);
            rollPlatformUpIntoView(generatedPrefab, spawnPosition);
            generateNewPlatFormFlag = false;
        }
        
    }

    private void rollPlatformUpIntoView(GameObject objectToMove, Transform spawnPosition){
        float step = speed * Time.deltaTime;
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, spawnPosition.position, step);
    }
    private GameObject generatePreFabAtSpawnPosition(GameObject prefabToUse, Transform spawnPosition){
        GameObject gameObjectPrefab = Instantiate(prefabToUse, spawnPosition.position, spawnPosition.rotation);
        Vector3 underLevelHeight = new Vector3(gameObjectPrefab.transform.position.x,
                    gameObjectPrefab.transform.position.y-20, 
                    gameObjectPrefab.transform.position.z);
        gameObjectPrefab.transform.position = underLevelHeight; 
        return gameObjectPrefab;
    }

    private Transform getSpawnPositions(){
        GameObject spawnLocationParent = proceduralPrefab.transform.Find("SpawnLocations").gameObject;
        List<Transform> spawnLocations = new List<Transform>();
        for (int i = 0; i < spawnLocationParent.transform.childCount; i++){
            spawnLocations.Add(spawnLocationParent.transform.GetChild(0));
        }
        return spawnLocations[(Random.Range(0, spawnLocations.Capacity))];
    }

    private GameObject getPreFabToUse(){
       GameObject preFabsParent = proceduralPrefab.transform.Find("PreFabs").gameObject;
        List<GameObject> prefabPositions = new List<GameObject>();
        for (int i = 0; i < preFabsParent.transform.childCount; i++){
            prefabPositions.Add(preFabsParent.transform.GetChild(0).gameObject);
        } 
        return prefabPositions[(Random.Range(0, prefabPositions.Capacity))];
        
    }

    public void triggerProceduralGenerationStep(){
        generateNewPlatFormFlag = true;
    }
}
