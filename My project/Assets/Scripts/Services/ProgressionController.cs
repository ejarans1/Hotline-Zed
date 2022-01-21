using UnityEngine;

public class ProgressionController : MonoBehaviour
{

    public bool levelClearedFlag = false;
    public bool interactedServiceFlag = false;
    public GameObject newBuildingPrefab;
    public GameObject progressionOrbPrefab;
    public GameObject progressionOrbInstance;
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
        calculateLevelClearedFlag();
        runLevelClearedActions();
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
            interactedServiceFlag = false;
        
        }

        if(!levelClearedFlag){
            return;
        } 
    }
    
    private void refreshLevel(){
        Destroy(oldBuildingSpawn);
        Destroy(progressionOrbInstance);
        levelClearedFlag = false;
        Transform spawnPointToSpawn = locationSpawner.returnOrderedSpawnPoint();
        GameObject enemyToReparent = spawnNewEnemy(spawnPointToSpawn);
        progressionOrbInstance = setProgressionControllerViaResource(spawnPointToSpawn);
        oldBuildingSpawn = spawnNewBuilding(spawnPointToSpawn);
        enemyToReparent.transform.SetParent(enemyGameObjectParents);
        interactableToProgressionService.setInteractableObject(progressionOrbInstance);
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

    public bool GetLevelClearedFlag(){
        return levelClearedFlag;
    }

    public void SetInteractedServiceFlag(bool hasInteracted){
        interactedServiceFlag = hasInteracted;
    }

    public GameObject setProgressionControllerViaResource(Transform spawnPoint){
        GameObject interacterSpawn = Instantiate(progressionOrbPrefab, spawnPoint.position, spawnPoint.rotation);
        // Get
        Interacter newInteracter = (Interacter)interacterSpawn.GetComponent(typeof(Interacter));
        newInteracter.setProgressionController(this);
        newInteracter.setInteractorService(interactableToProgressionService);
        newInteracter.setPlayer(player);
        newInteracter.setLayerMask();
        newInteracter.setHasInteracted(false);
        interactableToProgressionService.setInteractor(newInteracter);

        return interacterSpawn;
    }
}
