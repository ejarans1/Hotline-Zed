using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableToProgressionService : MonoBehaviour
{
    // Start is called before the first frame update

    public ProgressionController controller;

    public Transform player;

    public Transform interactableObject;

    public float interactRange;

    public bool hasInteracted = false;
    public LayerMask itemLayers;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.getLevelClearedFlag() == true){
            Collider[] playerSphereColliders = Physics.OverlapSphere(player.position, interactRange, itemLayers);
            hasInteracted = true;
            foreach(Collider enemy in playerSphereColliders){
                Destroy(interactableObject.gameObject);
            }
        }
    }

    public bool getHasInteracted(){
        return hasInteracted;
    }
}
