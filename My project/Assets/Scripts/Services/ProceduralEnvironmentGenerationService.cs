using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralEnvironmentGenerationService : MonoBehaviour
{
    public ProgressionController progressionController;
    public GameObject proceduralPrefab;

    public Transform environmentParent;

    public GameObject initialGameTile;

    bool generateNewPlatFormFlag = false;

    bool rollPlatformFlag = false;

    public Transform currentTilePosition;
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
            generateNewPlatform();
            updateServiceWithNewPlatformValues();
            //Setup Connections Between New Object and Generation Service
        }
    }

    private void updateServiceWithNewPlatformValues(){

    }
    private void generateNewPlatform(){
        generateNewPlatFormFlag = false; 
        prefabToUse = proceduralPrefab;
        generatedPrefab = generatePreFabAtSpawnPosition(prefabToUse, currentTilePosition);
        EnvironmentTileMover environmentTileMoverOfGeneratedPrefab = generatedPrefab.GetComponent<EnvironmentTileMover>();
        environmentTileMoverOfGeneratedPrefab.setPositionToMoveTowards(generatePositionWithOffset(currentTilePosition, 40, 0, 0));
        environmentTileMoverOfGeneratedPrefab.triggerTileMovement();
        generatedPrefab.transform.Find("InteractactableProgressionOrbInstance");
        progressionController.SetSpawnOrb(generatedPrefab.transform.Find("InteractactableProgressionOrbInstance").gameObject);
    }
    private GameObject generatePreFabAtSpawnPosition(GameObject prefabToUse, Transform  spawnPosition){
        Vector3 offsetPosition = generatePositionWithOffset(spawnPosition, 40, -20, 0);
        GameObject gameObjectPrefab = Instantiate(prefabToUse, offsetPosition, spawnPosition.rotation, environmentParent);
        return gameObjectPrefab;
    }

    private Vector3 generatePositionWithOffset(Transform originalPosition, float xOffset, float yOffset, float zOffset){
        float x = originalPosition.transform.position.x + xOffset;
        float y = originalPosition.transform.position.y + yOffset;
        float z = originalPosition.transform.position.z + zOffset;
        return new Vector3(x,y,z);
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

    public GameObject getCurrentTileGameObject(){
        return generatedPrefab;
    }
}
