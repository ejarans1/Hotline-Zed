using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwingTracker : MonoBehaviour
{

    bool handleSwingModeFlag = false;

    int swingModeTriggerCount = 0;

    public Camera camera;

    public Transform player;

    private Transform enemy;

    private Vector3 swingLineSpawnStartandLimit;
    // Start is called before the first frame update
    void Start()
    {
        
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
                stopAllMovement(player, enemy);
                swingModeTriggerCount++;    
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
        generateInFrontOfPlayer(swingLineSpawnStartandLimit);
        bool wasEnemyHit = calculateEnemyHitFlag();
        if(wasEnemyHit){
            Destroy(enemy1);
        }

        destroyRemainingSpheres();
        
        startMovementAgain();

             
    }

    private Vector3 calculateSwingLineSpawnAndLimit(Transform enemy){
        Vector3 mousePoint = getMousePosition();
        //float zCoordLimit = obtainZCoordinateLimitForEnemy(enemy);
        Vector3 swingLineSpawnStartAndLimit = new Vector3(mousePoint.x, mousePoint.y, 5);
        return swingLineSpawnStartAndLimit;
    }

    private bool calculateExitSwing (){
        return false;
    }

    private void destroyRemainingSpheres(){
        
    }

    private void startMovementAgain(){

    }

    private void generateLineAtMousePoint(Vector3 positionToGenerate){
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.parent = camera.transform;
        sphere.transform.position = positionToGenerate;
        
    }

    private void generateInFrontOfPlayer(Vector3 positionToGenerate){
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = camera.transform.forward;
        Quaternion playerRotation = camera.transform.rotation;
        float spawnDistance = 10;

         
 
        Vector3 spawnPos = playerPos + playerDirection*spawnDistance;
        Vector3 swingLineSpawnStartAndLimit = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z);
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.rotation = playerRotation; 
        sphere.transform.position = swingLineSpawnStartAndLimit;
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

    
}
