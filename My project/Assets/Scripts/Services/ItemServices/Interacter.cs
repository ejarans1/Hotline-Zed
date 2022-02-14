using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interacter : MonoBehaviour
{

    public ProgressionController progressionController;
    public Transform player;
    public CharacterStatService characterStatService;
    public InventoryUIHandler inventoryUIHandler;
    public bool hasInteracted = false;
    public float interactRange;
    public LayerMask itemLayers;

    public GameObject instanceOrb;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void FixedUpdate(){
    //     if (progressionController.GetLevelClearedFlag() == true){
    //         Collider[] playerSphereColliders = Physics.OverlapSphere(player.position, interactRange, itemLayers);
    //         foreach(Collider interactCollider in playerSphereColliders){
    //             string pickUpTag  = determinePickUpType(interactCollider);
    //             processPickupByTag(pickUpTag, interactCollider);
    //         }
    //     }
    // }

        void OnTriggerEnter(Collider other)
    {
        Collider[] playerSphereColliders = Physics.OverlapSphere(player.position, interactRange, itemLayers);
        foreach (Collider interactCollider in playerSphereColliders)
        {
            string pickUpTag = determinePickUpType(interactCollider);
            processPickupByTag(pickUpTag, interactCollider);
        }
    }

    public bool getHasInteracted(){
        return hasInteracted;
    } 

    private string determinePickUpType(Collider interactedObject){
        string pickUpType = null;
        if (interactedObject.gameObject.tag == "ProgressionPickup"){
            pickUpType = "ProgressionPickup";
        }
        if (interactedObject.gameObject.tag == "HealthPickup"){
            pickUpType = "HealthPickup";
        }
        if (interactedObject.gameObject.tag == "InventoryPickup"){
            pickUpType = "InventoryPickup";
        }
        return pickUpType;
    }

    private void processPickupByTag(string pickUpTag, Collider interactCollider){
        if (pickUpTag == "ProgressionPickup"){
            progressionController.SetInteractedServiceFlag(true);
        }
        if (pickUpTag == "HealthPickup"){
            characterStatService.incrementHealthByAmount(10f);
            Destroy(interactCollider.gameObject);

        }
        if (pickUpTag == "InventoryPickup"){
            Debug.Log(interactCollider.name);
            InventoryItem inventoryItem = interactCollider.gameObject.GetComponent<InventoryItem>();
            Image inventorySprite = inventoryItem.getSpriteImage();
            inventoryUIHandler.populateSingleItemToInventory(inventorySprite);
            Destroy(interactCollider.gameObject);
        }
    }

    public void setHasInteracted(bool valueToSet){
        hasInteracted = valueToSet;
    }

    public void setProgressionController(ProgressionController progressionControllerService){
        progressionController = progressionControllerService;
    }

    public void setPlayer(Transform existingPlayer){
        player = existingPlayer;
    }


    public void setLayerMask(){
        itemLayers = LayerMask.GetMask("interactableItemLayer");
        
    }

 
}
