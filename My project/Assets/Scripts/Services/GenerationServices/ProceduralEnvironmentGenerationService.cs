using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralEnvironmentGenerationService : MonoBehaviour
{
    public EnvironmentTileTrackerService environmentTileTrackerService;

    public RandomPrefabService randomPrefabService;
    public ProgressionController progressionController;
    public GameObject proceduralPrefab;
    GameObject generatedPrefab;
    public List<GameObject> environmentSpawnersObjects = new List<GameObject>();
    public Transform environmentParent;
    public Transform currentTilePosition;
    bool generateNewPlatFormFlag = false;
    bool rollPlatformFlag = false;
    float tileCount = 0;



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

    void Update(){

    }

    public void performGenerationStep(){
        if (generateNewPlatFormFlag){
            generateNewPlatFormFlag = false;
            int currentXTilePosition = environmentTileTrackerService.getCurrentXTilePosition();
            int currentYTilePosition = environmentTileTrackerService.getCurrentYTilePosition();
            environmentTileTrackerService.setMatrixValue(currentXTilePosition, currentYTilePosition);
            Vector3 spawnPosition = getSpawnPositions(currentTilePosition); 
            Debug.Log("Current New Spawn Position Is: " + spawnPosition);
            generateNewPlatform(spawnPosition);
            moveGeneratedTile(generatedPrefab);
            activateEnemySpawners(generatedPrefab);
            linkGeneratedPrefabToProgressionController();
            updateProceduralServiceWithNewPlatformValues();
            tileCount++;
        }
    }

    private void activateEnemySpawners(GameObject generatedPrefabToActivate){
        List <GameObject> enemySpawners = findEnemySpawners(generatedPrefabToActivate);
        foreach (GameObject enemySpawner in enemySpawners ){
            EnemySpawnerService enemySpawnerService = enemySpawner.GetComponent<EnemySpawnerService>();
            enemySpawnerService.setActiveFlag(true);
        }
    }

    private List<GameObject> findEnemySpawners (GameObject generatedPrefabWithChildren){
        List<GameObject> enemySpawners = FindObjectwithTag("EnemySpawner" ,generatedPrefabWithChildren.transform);
        return enemySpawners;
    }

    private void updateProceduralServiceWithNewPlatformValues(){
        currentTilePosition = generatedPrefab.transform;

    }
    private void generateNewPlatform(Vector3 spawnPosition){
        int currentXTilePosition = environmentTileTrackerService.getCurrentXTilePosition();
        int currentYTilePosition = environmentTileTrackerService.getCurrentYTilePosition();
        generateTillSuccessful(currentYTilePosition, currentXTilePosition,spawnPosition);
        updateTileTrackerServiceForTilePosition(currentXTilePosition, currentYTilePosition);
        updateTileTrackerServiceMatrix(currentXTilePosition, currentYTilePosition);       
    }

    private void generateTillSuccessful(int currentY, int currentX, Vector3 spawnPosition){
        bool isValidFlag = false;
        while(!isValidFlag){
            proceduralPrefab = randomPrefabService.getRandomPrefab();
            generatedPrefab = generatePreFabAtSpawnPosition(proceduralPrefab, spawnPosition, currentTilePosition);
            Debug.Log("Generating Till Successful, Position Is: " + generatedPrefab.transform.position);
            updateTileTrackerServiceForTilePosition(currentX, currentY);
            updateTileTrackerServiceMatrix(currentX, currentY);
            if (calculateValidPosition(currentX, currentY) == true){
                isValidFlag = false; 
                Destroy(generatedPrefab);
            }
            else {
                isValidFlag = true;
            }  
        }
    
    }

    private GameObject randomizeTilePrefab(){
        return randomPrefabService.getRandomPrefab();
    }

    private bool checkForValidPosition(int xPosition, int yPosition){
        return checkTileAvailability(xPosition, yPosition);
        
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

    private bool calculateValidPosition(int xPosition, int yPosition){
        Direction eastWestCalculation = calculateEastWestDirection();
        Direction northSouthCalculation = calculateNorthSouthDirection();
        int yAxisChange = calculateDirectionYAxisChange(northSouthCalculation);
        int xAxisChange = calculateDirectionXAxisChange(eastWestCalculation);
        int updatedXAxisPosition = xPosition + xAxisChange;
        int updatedYAxisPosition = yPosition + yAxisChange;
        bool isMatrixValueOccupied = environmentTileTrackerService.getMatrixValue(updatedXAxisPosition, updatedYAxisPosition);
        return isMatrixValueOccupied;
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
        float initialYPosition = initialTilePosition.position.z;
        float newYPosition = initialTilePosition.position.z;
        float comparedYValue = initialYPosition - newYPosition;
        if(comparedYValue > 0){
            return Direction.NORTH;
        }
        else {
            return Direction.SOUTH;
        }

    }
    private void  linkGeneratedPrefabToProgressionController(){
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

    public float getTileCount(){
        return tileCount;
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

    private List<GameObject> FindObjectwithTag(string _tag, Transform parent)
     {
         environmentSpawnersObjects.Clear();
         GetChildObject(parent, _tag);
         return environmentSpawnersObjects;
     }
 
    private void GetChildObject(Transform parent, string _tag)
     {
         for (int i = 0; i < parent.childCount; i++)
         {
             Transform child = parent.GetChild(i);
             if (child.tag == _tag)
             {
                environmentSpawnersObjects.Add(child.gameObject);
             }
             if (child.childCount > 0)
             {
                 GetChildObject(child, _tag);
             }
         }
     }
}
