using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralEnvironmentGenerationService : MonoBehaviour
{
    public ProgressionController progressionController;
    public GameObject proceduralPrefab;

    public Transform environmentParent;
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
            Vector3 spawnPosition = getSpawnPositions(currentTilePosition); 
            generateNewPlatform(spawnPosition);
            moveGeneratedTile(generatedPrefab);
            linkGeneratedPrefabToProgressionController();
            updateProceduralServiceWithNewPlatformValues();
            //Setup Connections Between New Object and Generation Service
        }
    }

    private void updateProceduralServiceWithNewPlatformValues(){
        currentTilePosition = generatedPrefab.transform;
        //currentTilePosition.transform.position = generatePositionWithOffset(generatedPrefab.transform, 0, 0, 0);
    }
    private void generateNewPlatform(Vector3 spawnPosition){
        generateNewPlatFormFlag = false; 
        prefabToUse = proceduralPrefab;
        generatedPrefab = generatePreFabAtSpawnPosition(prefabToUse, spawnPosition, currentTilePosition);
        
    }

    private void linkGeneratedPrefabToProgressionController(){
        progressionController.SetSpawnOrb(generatedPrefab.transform.Find("InteractactableProgressionOrbInstance").gameObject);
    }

    private void moveGeneratedTile(GameObject prefabToMove){
        Vector3 newPosition = generatePositionWithOffset(generatedPrefab.transform, 0, 20, 0); 
        EnvironmentTileMover environmentTileMoverOfGeneratedPrefab = generatedPrefab.GetComponent<EnvironmentTileMover>();;
        environmentTileMoverOfGeneratedPrefab.setPositionToMoveTowards(newPosition);
        environmentTileMoverOfGeneratedPrefab.triggerTileMovement();
    }
    private GameObject generatePreFabAtSpawnPosition(GameObject prefabToUse, Vector3  spawnPosition, Transform tileRotation){
        GameObject gameObjectPrefab = Instantiate(prefabToUse, spawnPosition, tileRotation.rotation, environmentParent);
        return gameObjectPrefab;
    }

    private Vector3 generatePositionWithOffset(Transform originalPosition, float xOffset, float yOffset, float zOffset){
        float x = originalPosition.transform.position.x + xOffset;
        float y = originalPosition.transform.position.y + yOffset;
        float z = originalPosition.transform.position.z + zOffset;
        return new Vector3(x,y,z);
    }
    //WorldTileService Creation is neccessary to track tile positioning using matrix array
    private Vector3 getSpawnPositions(Transform originalPosition){
        List<Vector3> spawnLocations = new List<Vector3>();
        spawnLocations.Add(generatePositionWithOffset(originalPosition, 40, -20, 0));
        spawnLocations.Add(generatePositionWithOffset(originalPosition, -40, -20, 0));
        spawnLocations.Add(generatePositionWithOffset(originalPosition, 0, -20, 40));
        spawnLocations.Add(generatePositionWithOffset(originalPosition, 0, -20, 40));
        
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
