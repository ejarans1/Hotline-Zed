using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{

    public InteractableToProgressionService interactableToProgressionService;
    public ProgressionController progressionController;
    public Transform player;
    public bool hasInteracted = false;
    public float interactRange;
    public LayerMask itemLayers;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (progressionController.GetLevelClearedFlag() == true){
            Collider[] playerSphereColliders = Physics.OverlapSphere(player.position, interactRange, itemLayers);
            foreach(Collider interactCollider in playerSphereColliders){
                hasInteracted = true;
            }
        }
    }

    public bool getHasInteracted(){
        return hasInteracted;
    }

    public void setHasInteracted(bool valueToSet){
        hasInteracted = valueToSet;
    }

    public void setProgressionController(ProgressionController progressionControllerService){
        progressionController = progressionControllerService;
    }
    public void setInteractorService(InteractableToProgressionService interactService){
        interactableToProgressionService = interactService;
    }

    public void setPlayer(Transform existingPlayer){
        player = existingPlayer;
    }


    public void setLayerMask(){
        itemLayers = LayerMask.GetMask("interactableItemLayer");
        
    }


}
