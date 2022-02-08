using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionService : MonoBehaviour
{
    public bool hasInteracted = false;
    public float interactRange;
    public LayerMask hitMarkerLayers;

    public GameObject xpOrbPrefab;
 
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
            GameObject generatedXpOrb = generatePreFabAtSpawnPosition(xpOrbPrefab, this.gameObject.transform.position, gameObject.transform.rotation);
            generatedXpOrb.tag = "XP";
            Destroy(interactCollider.gameObject);
        }        
    }

     private GameObject generatePreFabAtSpawnPosition(GameObject prefabToUse, Vector3  spawnPosition, Quaternion swingRotation){
        GameObject gameObjectPrefab = Instantiate(prefabToUse, spawnPosition, swingRotation);
        return gameObjectPrefab;
    }

}
