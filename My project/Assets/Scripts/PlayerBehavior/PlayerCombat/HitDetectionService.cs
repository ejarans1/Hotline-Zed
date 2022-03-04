using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HitDetectionService : MonoBehaviour
{
    public bool hasInteracted = false;
    public float interactRange;
    public LayerMask hitMarkerLayers;

    public GameObject xpOrbPrefab;

    public GameObject healthPickupPrefab;

    public GameObject inventoryPickupPrefab;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] playerSphereColliders = Physics.OverlapSphere(this.gameObject.transform.position,
        interactRange,
        hitMarkerLayers);
        foreach(Collider interactCollider in playerSphereColliders)
        {
            generateEnemyItemDrops(interactCollider.transform.position);
        }        
    }

    public void generateEnemyItemDrops(Vector3 spawnPosition)
    {
        generateXPOrb(spawnPosition);
        generateHealthPickup(spawnPosition);
        generateInventoryPickup(spawnPosition);
        
    }

    private void generateInventoryPickup(Vector3 spawnPosition)
    {
        GameObject generatedInventoryPickup = generatePreFabAtSpawnPosition(inventoryPickupPrefab,
            this.gameObject.transform.position,
            gameObject.transform.rotation);
        generatedInventoryPickup.tag = "InventoryPickup";
    }

    private void generateHealthPickup(Vector3 spawnPosition)
    {
        GameObject generatedHealthPickup = generatePreFabAtSpawnPosition(healthPickupPrefab,
            this.gameObject.transform.position,
            gameObject.transform.rotation);
        generatedHealthPickup.tag = "HealthPickup";
    }

    private void generateXPOrb(Vector3 spawnPosition)
    {
        GameObject generatedXpOrb = generatePreFabAtSpawnPosition(xpOrbPrefab,
            this.gameObject.transform.position,
            gameObject.transform.rotation);
        generatedXpOrb.tag = "XP";
    }

    private GameObject generatePreFabAtSpawnPosition(GameObject prefabToUse, Vector3  spawnPosition, Quaternion swingRotation){
        GameObject gameObjectPrefab = Instantiate(prefabToUse, spawnPosition, swingRotation);
        return gameObjectPrefab;
    }

}
