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
    public ProceduralEnvironmentGenerationService proceduralEnvironmentGenerationService;
    private GameObject oldBuildingSpawn;
    public float spawnDistance = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
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
            interactedServiceFlag = false;
            Destroy(progressionOrbInstance);
            proceduralEnvironmentGenerationService.triggerProceduralGenerationStep();
            Debug.Log("in run lvl cleared");
            proceduralEnvironmentGenerationService.performGenerationStep();
        }

        if(!levelClearedFlag){
            return;
        } 
    }

    private GameObject spawnNewBuilding(Transform spawnPoint) {

        return Instantiate(newBuildingPrefab, spawnPoint.position, spawnPoint.rotation);
        
    }

    private GameObject spawnNewEnemy(Transform spawnPoint) {
    
        return Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        
    }

    public bool GetLevelClearedFlag(){
        return levelClearedFlag;
    }

    public void SetInteractedServiceFlag(bool hasInteracted){
        interactedServiceFlag = hasInteracted;
    }

    public void SetSpawnOrb(GameObject newProgressionOrbInstance){
        progressionOrbInstance = newProgressionOrbInstance;
    }
}
