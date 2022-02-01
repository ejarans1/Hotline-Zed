using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToPlayerService : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform transformToSet; 

    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    public float camSens = 0.25f; //How sensitive it with mouse

    public bool needToUpdatePosition = false;

    public Transform weaponTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
