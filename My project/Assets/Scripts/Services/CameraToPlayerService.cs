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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (needToUpdatePosition){
            updatePosition();
            updateMousePosition();
        }
    }

    public void updatePosition(){
        gameObject.transform.position = transformToSet.position;
        gameObject.transform.rotation = transformToSet.rotation;
    }

    public void setTransform(Transform newTransform){
        transform.position = newTransform.position;
        transform.rotation = newTransform.rotation;
    }

    public void setUpdateFlag(bool flagValue){
        needToUpdatePosition = flagValue;
    }

    private void updateMousePosition(){
        lastMouse = Input.mousePosition - lastMouse ;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0 );
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse =  Input.mousePosition;
        
    }
}
