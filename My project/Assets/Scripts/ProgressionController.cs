using UnityEngine;

public class ProgressionController : MonoBehaviour
{

    public bool levelClearedFlag = false;

    public bool hasInteracted = false;

    public GameObject objectToSpawn;

    public GameObject enemyToSpawn;

    public Transform player;
    
    public Transform enemyGameObjectParents;

    public InteractableToProgressionService interactableToProgressionService;

    public GameObject orbSpawnEnemy;

    public GameObject oldBuildingSpawn;
    public float spawnDistance = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        calculateLevelGatewayFlag();
        calculateLevelClearedFlag();

        if(hasInteracted){
            runLevelClearedActions();
        }
        
        
        

    }

    public void runLevelClearedActions(){
        if(levelClearedFlag == true){
            refreshLevel();
        
        }

        if(levelClearedFlag == false){
            return;
        } 
    }



    public void calculateLevelGatewayFlag(){
        interactableToProgressionService.getHasInteracted();
    }
    public void refreshLevel(){
        Destroy(oldBuildingSpawn);
        levelClearedFlag = false;
        GameObject enemyToReparent = spawnNewEnemy();
        oldBuildingSpawn = spawnNewBuilding();
        enemyToReparent.transform.SetParent(enemyGameObjectParents);
    }

    public void calculateLevelClearedFlag(){
        if(enemyGameObjectParents.childCount == 0){
            levelClearedFlag = true;
        } 

        if(enemyGameObjectParents.childCount != 0){
            levelClearedFlag = false;
        }
    }

    public GameObject spawnNewBuilding() {
        Quaternion playerRotation = player.transform.rotation;
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        
        Vector3 spawnPos = playerPos + playerDirection*spawnDistance;

        return Instantiate(objectToSpawn, spawnPos, playerRotation);
        
    }

    public GameObject spawnNewEnemy() {
        Quaternion playerRotation = player.transform.rotation;
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        
        Vector3 spawnPos = playerPos + playerDirection*spawnDistance;

        return Instantiate(enemyToSpawn, spawnPos, playerRotation);
        
    }

    public bool getLevelClearedFlag(){
        return levelClearedFlag;
    }
}
