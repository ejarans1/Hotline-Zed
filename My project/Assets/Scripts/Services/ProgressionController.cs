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
    public LocationSpawner locationSpawner;
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
        Transform spawnPointToSpawn = locationSpawner.returnRandomSpawnPoint();
        GameObject enemyToReparent = spawnNewEnemy(spawnPointToSpawn);
        GameObject refreshedInteractSphere = spawnNewInteractSphere(spawnPointToSpawn);
        oldBuildingSpawn = spawnNewBuilding(spawnPointToSpawn);
        enemyToReparent.transform.SetParent(enemyGameObjectParents);
        interactableToProgressionService.setHasInteracted(false);
        interactableToProgressionService.setInteractableObject(refreshedInteractSphere);
    }

    private GameObject spawnNewBuilding(Transform spawnPoint) {

        return Instantiate(newBuildingPrefab, spawnPoint.position, spawnPoint.rotation);
        
    }

    private GameObject spawnNewEnemy(Transform spawnPoint) {
    
        return Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        
    }
    private GameObject spawnNewInteractSphere(Transform spawnPoint){
        
        return Instantiate(progressionOrbPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public bool getLevelClearedFlag(){
        return levelClearedFlag;
    }
}
