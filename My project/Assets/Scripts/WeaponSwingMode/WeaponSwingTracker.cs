using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwingTracker : MonoBehaviour
{

    bool handleSwingModeFlag = false;

    public Camera camera;

    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int MouseButtonInput = getBaseInputForMouse();
        if (MouseButtonInput == 1){
            handleSwingMode();
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

    private void handleSwingMode(){
        handleSwingModeFlag = true;
        Debug.Log("handleSwingMode");
        Transform enemy = calculateTargetEnemy();
        stopAllMovement(player, enemy);
        //float zCoordLimit = obtainZCoordinateLimitForEnemy(enemy);
        Vector3 mousePoint = getMousePosition();
        Vector3 swingLineSpawnStartAndLimit = new Vector3(mousePoint.x, mousePoint.y, 5);

        while (handleSwingModeFlag){
            generateInFrontOfPlayer(swingLineSpawnStartAndLimit);
            handleSwingModeFlag = calculateExitSwing();
        }

        bool wasEnemyHit = calculateEnemyHitFlag();
        if(wasEnemyHit){
            Destroy(enemy);
        }

        destroyRemainingSpheres();
        
        startMovementAgain();

             
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
         Vector3 playerDirection = player.transform.forward;
         Quaternion playerRotation = player.transform.rotation;
         float spawnDistance = 10;
 
        Vector3 spawnPos = playerPos + playerDirection*spawnDistance;
 
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = spawnPos;
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
