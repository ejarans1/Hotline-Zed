using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableToProgressionService : MonoBehaviour
{
    // Start is called before the first frame update

    public ProgressionController progressionController;

    public Interacter interacter;

    public Transform player;

    public Transform interactableObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (progressionController.GetLevelClearedFlag() == true){
            if (interacter.getHasInteracted()){
                progressionController.SetInteractedServiceFlag(interacter.getHasInteracted());
            }    
        }
    }
    public void setInteractableObject(GameObject newInteractableObject){
        interactableObject = newInteractableObject.transform;
    }

    public void setInteractor(Interacter existingInteracter){
        interacter = existingInteracter;
    }

    
}
