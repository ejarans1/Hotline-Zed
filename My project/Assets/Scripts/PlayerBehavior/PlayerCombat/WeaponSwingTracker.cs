using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwingTracker : MonoBehaviour
{

    bool handleSwingModeFlag = false;

    int swingModeTriggerCount = 0;

    public Camera camera;

    public GameObject hitMarkerPrefab;

    public Transform player;
    
    private Vector3 swingModeStartPosition;

    private Transform enemy;

    private Vector3 swingLineSpawnStartandLimit;

    private List<GameObject> generatedHitMarkers;
    // Start is called before the first frame update
    void Start()
    {
        generatedHitMarkers = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        
        int MouseButtonInput = getBaseInputForMouse();
        if (MouseButtonInput == 1){
            if (swingModeTriggerCount == 0){
                handleSwingModeFlag = true;
                enemy = calculateTargetEnemy();
                swingLineSpawnStartandLimit = calculateSwingLineSpawnAndLimit(enemy);
                swingModeStartPosition = player.position;
                swingModeTriggerCount++; 
                destroyRemainingHitMarkers();
                generatedHitMarkers.Clear();  
            }
            else if(swingModeTriggerCount == 1) {
                swingModeTriggerCount = 0;
                handleSwingModeFlag = false;
            }
        }
        if (handleSwingModeFlag){
            handleSwingMode(enemy, swingLineSpawnStartandLimit);
        }
        
    }

    private int getBaseInputForMouse() {
        if(Input.GetKey(KeyCode.Mouse0)){
            return 0;
        }
        if(Input.GetKey(KeyCode.Mouse1)){
            return 1;
        }
        return -1;
    }

    private void handleSwingMode(Transform enemy1, Vector3 startPosition){
        GameObject hitMarker = generateInFrontOfPlayer(swingLineSpawnStartandLimit);
        addGeneratedPrefabToHitMarkerList(hitMarker);
        player.position = swingModeStartPosition;
        bool wasEnemyHit = calculateEnemyHitFlag();
        if(wasEnemyHit){
            Destroy(enemy1);
        }
        
        startMovementAgain();

             
    }

    private void addGeneratedPrefabToHitMarkerList(GameObject newHitMarker){
        generatedHitMarkers.Add(newHitMarker);
    }

    private Vector3 calculateSwingLineSpawnAndLimit(Transform enemy){
        Vector3 mousePoint = getMousePosition();
        //float zCoordLimit = obtainZCoordinateLimitForEnemy(enemy);
        Vector3 swingLineSpawnStartAndLimit = new Vector3(mousePoint.x, mousePoint.y, 1);
        return swingLineSpawnStartAndLimit;
    }

    private bool calculateExitSwing (){
        return false;
    }

    private void destroyRemainingHitMarkers(){
        foreach (GameObject hitMarkerToDestroy in generatedHitMarkers){
            Destroy(hitMarkerToDestroy);
        }
    }

    private void startMovementAgain(){

    }

    private GameObject generateInFrontOfPlayer(Vector3 positionToGenerate){
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = camera.transform.forward;
        Quaternion playerRotation = camera.transform.rotation;
        float spawnDistance = 5;


        Vector3 spawnPos = playerPos + playerDirection*spawnDistance;
        Vector3 swingLineSpawnStartAndLimit = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z);
        return generatePreFabAtSpawnPosition(hitMarkerPrefab, spawnPos, playerRotation);
    }

    private void stopAllMovement(Transform player1, Transform enemy1){
        //player1.transform.position = player1.transform.position;
        //enemy1.transform.position = enemy1.transform.position;
    }

    private float obtainZCoordinateLimitForEnemy(Transform enemy1){
        return enemy1.position.z;
    }

    private Vector3 getMousePosition(){
        return Input.mousePosition;
    }


    private Transform calculateTargetEnemy(){
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            
            return objectHit;
        }
        return null;
    }

    private bool calculateEnemyHitFlag(){
        return false;
    }

    private GameObject generatePreFabAtSpawnPosition(GameObject prefabToUse, Vector3  spawnPosition, Quaternion swingRotation){
        GameObject gameObjectPrefab = Instantiate(prefabToUse, spawnPosition, swingRotation);
        return gameObjectPrefab;
    }

    
}
