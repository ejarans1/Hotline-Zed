using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralEnvironmentGenerationService : MonoBehaviour
{
    public GameObject proceduralController;

    public GameObject proceduralPrefab;

    public Transform environmentParent;

    public GameObject initialGameTile;

    bool generateNewPlatFormFlag = false;

    bool rollPlatformFlag = false;

    public Transform spawnPoint1;

    Transform spawnPosition;
    GameObject prefabToUse;
    GameObject generatedPrefab;

    float speed = 15;
    float startTime;
    float journeyLength;


        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (generateNewPlatFormFlag){
            spawnPosition = spawnPoint1;
            prefabToUse = proceduralPrefab;
            float x = spawnPoint1.transform.position.x;
            float y = spawnPoint1.transform.position.y;
            float z = spawnPoint1.transform.position.z;
            generatedPrefab = generatePreFabAtSpawnPosition(prefabToUse, new Vector3(x+40, y-20, z));
            EnvironmentTileMover environmentTileMoverOfGeneratedPrefab = generatedPrefab.GetComponent<EnvironmentTileMover>();
            environmentTileMoverOfGeneratedPrefab.setPositionToMoveTowards(new Vector3(x+40, y, z));
            environmentTileMoverOfGeneratedPrefab.triggerTileMovement();
            generateNewPlatFormFlag = false;     
        }
    }
    private GameObject generatePreFabAtSpawnPosition(GameObject prefabToUse, Vector3 spawnPosition){
        float x = spawnPoint1.transform.position.x + 40;
        float y = spawnPoint1.transform.position.y + -20;
        float z = spawnPoint1.transform.position.z;
        GameObject gameObjectPrefab = Instantiate(prefabToUse, new Vector3(x, y, z), spawnPoint1.rotation, environmentParent);
        return gameObjectPrefab;
    }

    private Transform getSpawnPositions(){
        GameObject spawnLocationParent = initialGameTile.transform.Find("SpawnLocations").gameObject;
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
