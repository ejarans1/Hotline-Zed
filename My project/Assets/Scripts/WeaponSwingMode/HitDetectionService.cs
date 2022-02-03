using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionService : MonoBehaviour
{
    public bool hasInteracted = false;
    public float interactRange;
    public LayerMask hitMarkerLayers;
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
            Destroy(interactCollider.gameObject);
        }        
    }
}
