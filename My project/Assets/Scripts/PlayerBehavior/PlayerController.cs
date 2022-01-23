using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
 
    /*
    Writen by Windexglow 11-13-10.  Use it, edit it, steal it I don't care.  
    Converted to C# 27-02-13 - no credit wanted.
    Simple flycam I made, since I couldn't find any others made public.  
    Made simple to use (drag and drop, done) for regular keyboard layout  
    wasd : basic movement
    shift : Makes camera accelerate
    space : Moves camera on X and Z axis only.  So camera doesn't gain any height*/
     
     
    public float mainSpeed = 12.5f; //regular speed
    public float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    public float maxShift = 1000.0f; //Maximum speed when holdin gshift
    
    public float maxDistance = -25;
    public float minDistance = 25 ;
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun= 1.0f;

    public float camSens = 0.25f; //How sensitive it with mouse

    private bool invisibleWallFlagEast = true;

    private bool invisibleWallFlagWest = true;
    public Animator animator;

    public AttackController attackController;

    public Transform playerSpawnPoint;

    public Rigidbody playerRigidBody;

    public GameObject cameraPosition;
    private GameObject oldCameraPosition;
    public CameraToPlayerService cameraToPlayerService;

    
    
    void Update () {
        CalculateInvisibleWallPointWest();
        CalculateInvisibleWallPointEast();
        //updateMousePosition();
        updatePlayerAnimation();
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0){ // only move while a direction key is pressed
          if (Input.GetKey (KeyCode.LeftShift)){
              totalRun += Time.deltaTime;
              p  = p * totalRun * shiftAdd;
              p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
              p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
              p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
          } else {
              totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
              p = p * mainSpeed;
          }
         
          p = p * Time.deltaTime;
          if (!invisibleWallFlagEast) {
              transform.position = playerSpawnPoint.position;
              updatePlayerCameraPositionAndRotation();
          }
          if (!invisibleWallFlagWest) {
                transform.position = playerSpawnPoint.position;
                updatePlayerCameraPositionAndRotation();
          }
          float horizontalAxis = Input.GetAxis("Horizontal"); // Your X axis A and D keys
          float verticalAxis = Input.GetAxis("Vertical"); // Your Z axis W and S keys
          // Create force vector with 0 y and apply it to your rb
          playerRigidBody.AddForce (p);
        
        }
        updatePlayerCameraPositionAndRotation();
        invisibleWallCheck();
    }

    void LateUpdate(){

    }


    private void updateMousePosition(){
        lastMouse = Input.mousePosition - lastMouse ;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0 );
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse =  Input.mousePosition;
    }

    private void invisibleWallCheck(){
        if (!invisibleWallFlagEast) {
              transform.position = playerSpawnPoint.position;
              updatePlayerCameraPositionAndRotation();
          }
          if (!invisibleWallFlagWest) {
                transform.position = playerSpawnPoint.position;
                updatePlayerCameraPositionAndRotation();
          }
    }
    private void updatePlayerAnimation(){
        int MouseButtonInput = getBaseInputForMouse();
    
        if(MouseButtonInput == 0){
            animator.Play("swingball");
            attackController.attack();
        }
        if(MouseButtonInput == 1){
            animator.Play("one");
        }
    }
    

    private void updatePlayerCameraPositionAndRotation(){
        Transform transformForCameraPosition = cameraPosition.transform;
        Vector3 positionVectorForCamera = new Vector3(transform.position.x, cameraPosition.transform.position.y, transform.position.z);
        transformForCameraPosition.position = positionVectorForCamera;
        transformForCameraPosition.rotation = cameraPosition.transform.rotation;
        cameraToPlayerService.setTransform(transformForCameraPosition);
        cameraToPlayerService.setUpdateFlag(true);
    }
    
    private Vector3 GetBaseInput() { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey (KeyCode.W)){
            playerRigidBody.AddForce(0,0,10);
        }
        if (Input.GetKey (KeyCode.S)){
            playerRigidBody.AddForce(0,0,-10);
        }
        if (Input.GetKey (KeyCode.A)){
            playerRigidBody.AddForce(-10,0,0);
            
        }
        if (Input.GetKey (KeyCode.D)){
            playerRigidBody.AddForce(10,0,0);
        }
        return p_Velocity;
    }

    private int getBaseInputForMouse() {
        if(Input.GetKey(KeyCode.Mouse0)){
            return 0;
        }
        if(Input.GetKey(KeyCode.Mouse1)){
            return 1;
        }
        return -1;
    }



    private void CalculateInvisibleWallPointEast(){
        if(gameObject.transform.position.x >= maxDistance){
            Debug.Log(gameObject.transform.position.x);
            invisibleWallFlagEast = true;
        } else {
            invisibleWallFlagEast = false;
        }
    }
    private void CalculateInvisibleWallPointWest(){
        if(gameObject.transform.position.x <= minDistance){
            Debug.Log(gameObject.transform.position.x);
            invisibleWallFlagWest = true;
        } else {
            invisibleWallFlagWest = false;
        }
    }

}
