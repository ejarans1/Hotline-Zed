using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        foreach(Collider interactCollider in playerSphereColliders){
            GameObject generatedXpOrb = generatePreFabAtSpawnPosition(xpOrbPrefab,
                                                this.gameObject.transform.position,
                                                gameObject.transform.rotation);
            generatedXpOrb.tag = "XP";
            GameObject generatedHealthPickup = generatePreFabAtSpawnPosition(healthPickupPrefab,
                                                this.gameObject.transform.position,
                                                gameObject.transform.rotation);
            generatedHealthPickup.tag = "HealthPickup";
            GameObject generatedInventoryPickup = generatePreFabAtSpawnPosition(inventoryPickupPrefab,
                                                this.gameObject.transform.position,
                                                gameObject.transform.rotation);
            generatedInventoryPickup.tag = "InventoryPickup";
            Destroy(interactCollider.gameObject);
        }        
    }

     private GameObject generatePreFabAtSpawnPosition(GameObject prefabToUse, Vector3  spawnPosition, Quaternion swingRotation){
        GameObject gameObjectPrefab = Instantiate(prefabToUse, spawnPosition, swingRotation);
        return gameObjectPrefab;
    }

}
