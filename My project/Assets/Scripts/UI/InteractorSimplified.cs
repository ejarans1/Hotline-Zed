using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class InteractorSimplified : MonoBehaviour
{
    public Transform player;
    public InventoryUIHandler inventoryUIHandler;
    public float interactRange;
    public LayerMask itemLayers;
    public bool hasInteracted;
    // Start is called before the first frame update
    void Start()
    {
        hasInteracted = false;
    }

    // Update is called once per frame
    void Update()
    {

            
        
    }

    void OnTriggerEnter(Collider other)
    {
        Collider[] playerSphereColliders = Physics.OverlapSphere(player.position, interactRange, itemLayers);
        foreach (Collider interactCollider in playerSphereColliders)
        {
            string pickUpTag = determinePickUpType(interactCollider);
            processPickupByTag(pickUpTag, interactCollider);
        }
    }

    private string determinePickUpType(Collider interactedObject)
    {
        hasInteracted = true;
        string pickUpType = null;
        if (interactedObject.gameObject.tag == "ProgressionPickup")
        {
            pickUpType = "ProgressionPickup";
        }
        if (interactedObject.gameObject.tag == "HealthPickup")
        {
            pickUpType = "HealthPickup";
        }
        if (interactedObject.gameObject.tag == "InventoryPickup")
        {
            pickUpType = "InventoryPickup";
        }
        return pickUpType;
    }

    private void processPickupByTag(string pickUpTag, Collider interactCollider)
    {
        if (pickUpTag == "InventoryPickup")
        {
            Debug.Log(interactCollider.name);
            InventoryItem inventoryItem = interactCollider.gameObject.GetComponent<InventoryItem>();
            Image inventorySprite = inventoryItem.getSpriteImage();
            inventoryUIHandler.populateSingleItemToInventory(inventorySprite);
            Destroy(interactCollider.gameObject);
        }
    }
}
