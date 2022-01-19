using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public Transform player;

    public Transform interactableObject;

    public float interactRange;

    public GameObject gameObjectToSpawn;

    public float spawnDistance = 10;
    
    private bool destroyFlag = false;


    public LayerMask itemLayers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(destroyFlag == false){
            Collider[] playerSphereColliders = Physics.OverlapSphere(player.position, interactRange, itemLayers);
            foreach(Collider enemy in playerSphereColliders){

                Vector3 playerPos = player.transform.position;
                Vector3 playerDirection = player.transform.forward;
                Quaternion playerRotation = player.transform.rotation;

                Vector3 spawnPos = playerPos + playerDirection*spawnDistance;
                //p_Velocity += new Vector3(1, 0, 0); //Go Left
                Instantiate(gameObjectToSpawn, spawnPos, playerRotation );
                destroyFlag = true;
                Destroy(interactableObject.gameObject);
                return;
            }
        }
    }
}
