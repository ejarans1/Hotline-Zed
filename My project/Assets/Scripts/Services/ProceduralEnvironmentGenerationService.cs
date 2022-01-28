using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralEnvironmentGenerationService : MonoBehaviour
{
    public EnvironmentTileTrackerService environmentTileTrackerService;
    public ProgressionController progressionController;
    public GameObject proceduralPrefab;
    GameObject prefabToUse;
    GameObject generatedPrefab;
    public Transform environmentParent;
    public Transform currentTilePosition;
    bool generateNewPlatFormFlag = false;
    bool rollPlatformFlag = false;

    enum Direction
{
    NORTH,
    SOUTH,
    EAST,
    WEST
}
    

    void Start()
    {
        
    }

    void Update()
    {
        if (generateNewPlatFormFlag){
            Vector3 spawnPosition = getSpawnPositions(currentTilePosition); 
            generateNewPlatform(spawnPosition);
            moveGeneratedTile(generatedPrefab);
            linkGeneratedPrefabToProgressionController();
            updateProceduralServiceWithNewPlatformValues();
        }
    }

    private void updateProceduralServiceWithNewPlatformValues(){
        currentTilePosition = generatedPrefab.transform;
    }
    private void generateNewPlatform(Vector3 spawnPosition){
        int currentXTilePosition = environmentTileTrackerService.getCurrentXTilePosition();
        int currentYTilePosition = environmentTileTrackerService.getCurrentYTilePosition();
        generateNewPlatFormFlag = false; 
        prefabToUse = proceduralPrefab;
        generatedPrefab = generatePreFabAtSpawnPosition(prefabToUse, spawnPosition, currentTilePosition); 
        updateTileTrackerServiceForTilePosition(currentXTilePosition, currentYTilePosition);
        updateTileTrackerServiceMatrix(currentXTilePosition, currentYTilePosition);       

    }
        
    private void updateTileTrackerServiceMatrix(int xPosition, int yPosition){
        environmentTileTrackerService.setMatrixValue(xPosition,yPosition);
    }

    private void updateTileTrackerServiceForTilePosition(int xPosition, int yPosition){
        Direction eastWestCalculation = calculateEastWestDirection();
        Direction northSouthCalculation = calculateNorthSouthDirection();
        int yAxisChange = calculateDirectionYAxisChange(northSouthCalculation);
        int xAxisChange = calculateDirectionXAxisChange(eastWestCalculation);
        int updatedXAxisPosition = xPosition + xAxisChange;
        int updatedYAxisPosition = yPosition + yAxisChange;
        environmentTileTrackerService.setMatrixValue(updatedXAxisPosition, updatedYAxisPosition);
    }

    private int calculateDirectionXAxisChange(Direction eastWest){
        if (eastWest == Direction.EAST){
            return 1;
        }
        else {
            return -1;
        }
    }

    private int calculateDirectionYAxisChange(Direction northSouth){
        if (northSouth == Direction.NORTH){
            return 1;
        }
        else {
            return -1;
        }
    }

    private Direction calculateEastWestDirection(){
        return performPreTileComparisonForXAxis(currentTilePosition, generatedPrefab.transform);
    }

    private Direction performPreTileComparisonForXAxis (Transform initialTilePosition, Transform newTilePosition){
        float initialXPosition = initialTilePosition.position.x;
        float newXPosition = initialTilePosition.position.x;
        float comparedXValue = initialXPosition - newXPosition;
        if(comparedXValue > 0){
            return Direction.EAST;
        }
        else {
            return Direction.WEST;
        }

    }


    private Direction calculateNorthSouthDirection(){
        return performPreTileComparisonForYAxis(currentTilePosition, generatedPrefab.transform);
    }

    private Direction performPreTileComparisonForYAxis (Transform initialTilePosition, Transform newTilePosition){
        float initialYPosition = initialTilePosition.position.y;
        float newYPosition = initialTilePosition.position.y;
        float comparedYValue = initialYPosition - newYPosition;
        if(comparedYValue > 0){
            return Direction.NORTH;
        }
        else {
            return Direction.SOUTH;
        }

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

    private bool checkTileAvailability(int xAxis, int yAxis){
        return environmentTileTrackerService.getMatrixValue(xAxis, yAxis);
    }
}
