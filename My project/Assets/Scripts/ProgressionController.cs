using UnityEngine;

public class ProgressionController : MonoBehaviour
{

    public bool levelClearedFlag = false;
    public bool interactedServiceFlag = false;
    public GameObject newBuildingPrefab;
    public GameObject progressionOrbPrefab;
    public GameObject enemyPrefab;
    public Transform player;
    public Transform enemyGameObjectParents;
    public InteractableToProgressionService interactableToProgressionService;
    private GameObject oldBuildingSpawn;
    public float spawnDistance = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        calculateHasInteractedFlag();
        calculateLevelClearedFlag();
        runLevelClearedActions();
    }

    public void calculateHasInteractedFlag(){
        interactedServiceFlag = interactableToProgressionService.getHasInteracted();
    }

    public void calculateLevelClearedFlag(){
        if(enemyGameObjectParents.childCount == 0){
            levelClearedFlag = true;
        } 

        if(enemyGameObjectParents.childCount != 0){
            levelClearedFlag = false;
        }
    }

    public void runLevelClearedActions(){
        if(levelClearedFlag && interactedServiceFlag){
            refreshLevel();
        
        }

        if(!levelClearedFlag){
            return;
        } 
    }
    
    private void refreshLevel(){
        Destroy(oldBuildingSpawn);
        levelClearedFlag = false;
        GameObject enemyToReparent = spawnNewEnemy();
        GameObject refreshedInteractSphere = spawnNewInteractSphere();
        oldBuildingSpawn = spawnNewBuilding();
        enemyToReparent.transform.SetParent(enemyGameObjectParents);
        interactableToProgressionService.setHasInteracted(false);
        interactableToProgressionService.setInteractableObject(refreshedInteractSphere);
    }

    private GameObject spawnNewBuilding() {
        Quaternion playerRotation = player.transform.rotation;
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        Vector3 spawnPos = playerPos + playerDirection*spawnDistance;

        return Instantiate(newBuildingPrefab, spawnPos, playerRotation);
        
    }

    private GameObject spawnNewEnemy() {
        Quaternion playerRotation = player.transform.rotation;
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        
        Vector3 spawnPos = playerPos + playerDirection*spawnDistance;

        return Instantiate(enemyPrefab, spawnPos, playerRotation);
        
    }
    private GameObject spawnNewInteractSphere(){
        Quaternion playerRotation = player.transform.rotation;
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        
        Vector3 spawnPos = playerPos + playerDirection*spawnDistance;

        return Instantiate(newBuildingPrefab, spawnPos, playerRotation);
    }

    public bool getLevelClearedFlag(){
        return levelClearedFlag;
    }
}
