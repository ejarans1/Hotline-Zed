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
    public float negativeXLimit = -25;
    public float positiveXLimit = 25 ;
    public float negativeYLimit = -25;
    public float positiveYLimit = 25 ;
    public float negativeZLimit = -25;
    public float positiveZLimit = 25 ;

    public float transformOffset = 2;
    
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun= 1.0f;
    public float camSens = 0.25f; //How sensitive it with mouse

    private bool invisWallPositiveXFlag = true;
    private bool invisWallNegativeXFlag = true;

    private bool invisWallPositiveYFlag = true;
    private bool invisWallNegativeYFlag = true;

    private bool invisWallPositiveZFlag = true;
    private bool invisWallNegativeZFlag = true;
    public Animator animator;

    public AttackController attackController;

    public Transform playerSpawnPoint;

    public Rigidbody playerRigidBody;

    public Transform cameraPosition;

    public Transform weapon;

    public AudioHandler audioHandler; 

    
    
    void Update () {
        if (Input.GetKeyDown(KeyCode.W)) {
             audioHandler.playFootSStepAudioSource();
         }
      
         if (Input.GetKeyUp(KeyCode.W)) {
             audioHandler.stopFootStepAudioSource();
         }
    }

    void FixedUpdate(){
        calculateInvisibleWallViolations();
        updatePlayerAnimation();
        applyForce();
        updatePlayerCameraPositionAndRotation();
        invisibleWallCheck();
    }


    private void applyForce(){
        Vector3 p = GetBaseInput();
    }

    private Vector3 calculateSpeed(Vector3 p){
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
          return p;
    }

    void LateUpdate(){

    }

    private void calculateInvisibleWallViolations(){
        CalculateInvisibleWallNegativeX();
        CalculateInvisibleWallPositiveX();
        CalculateInvisibleWallPositiveY();
        CalculateInvisibleWallNegativeY();
        CalculateInvisibleWallPositiveZ();
        CalculateInvisibleWallNegativeZ();
    }

    private void invisibleWallCheck(){
        if (!invisWallPositiveXFlag) {
              transform.position = playerSpawnPoint.position;
              updatePlayerCameraPositionAndRotation();
          }
          if (!invisWallNegativeXFlag) {
                transform.position = playerSpawnPoint.position;
                updatePlayerCameraPositionAndRotation();
          }
          if (!invisWallPositiveYFlag) {
              transform.position = playerSpawnPoint.position;
              updatePlayerCameraPositionAndRotation();
          }
          if (!invisWallNegativeYFlag) {
                transform.position = playerSpawnPoint.position;
                updatePlayerCameraPositionAndRotation();
          }
          if (!invisWallPositiveZFlag) {
              transform.position = playerSpawnPoint.position;
              updatePlayerCameraPositionAndRotation();
          }
          if (!invisWallNegativeZFlag) {
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
            animator.Play("IdleWeapon");
        }
    }
    

    private void updatePlayerCameraPositionAndRotation(){
        updatePosition(this.transform);
        updateMousePosition();
        
    }

    
    
    private Vector3 GetBaseInput() { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey (KeyCode.W)){
            Vector3 cameraForward = cameraPosition.transform.forward;
            playerRigidBody.AddForce(cameraForward*20);
        }
        if (Input.GetKey (KeyCode.S)){
            Vector3 cameraBackward = -cameraPosition.transform.forward;
            playerRigidBody.AddForce(cameraBackward*20);
        }
        if (Input.GetKey (KeyCode.A)){
            Vector3 cameraLeft = -cameraPosition.transform.right;
            playerRigidBody.AddForce(cameraLeft*20);
            
        }
        if (Input.GetKey (KeyCode.D)){
            Vector3 cameraRight = cameraPosition.transform.right;
            playerRigidBody.AddForce(cameraRight*20);
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



    private void CalculateInvisibleWallPositiveX(){
        if(gameObject.transform.position.x >= negativeXLimit){
            //Debug.Log(gameObject.transform.position.x);
            invisWallPositiveXFlag = true;
        } else {
            invisWallPositiveXFlag = false;
        }
    }
    private void CalculateInvisibleWallNegativeX(){
        if(gameObject.transform.position.x <= positiveXLimit){
            //Debug.Log(gameObject.transform.position.x);
            invisWallNegativeXFlag = true;
        } else {
            invisWallNegativeXFlag = false;
        }
    }

    private void CalculateInvisibleWallPositiveY(){
        if(gameObject.transform.position.y >= negativeYLimit){
            //Debug.Log(gameObject.transform.position.y);
            invisWallPositiveYFlag = true;
        } else {
            invisWallPositiveYFlag = false;
        }
    }
    private void CalculateInvisibleWallNegativeY(){
        if(gameObject.transform.position.y <= positiveYLimit){
            invisWallNegativeYFlag = true;
        } else {
            invisWallNegativeYFlag = false;
        }
    }

    private void CalculateInvisibleWallPositiveZ(){
        if(gameObject.transform.position.z >= negativeZLimit){
            //Debug.Log(gameObject.transform.position.z);
            invisWallPositiveZFlag = true;
        } else {
            invisWallPositiveZFlag = false;
        }
    }
    private void CalculateInvisibleWallNegativeZ(){
        if(gameObject.transform.position.z <= positiveZLimit){
            //Debug.Log(gameObject.transform.position.z);
            invisWallNegativeZFlag = true;
        } else {
            invisWallNegativeZFlag = false;
        }
    }

    private void updatePosition(Transform transformToSet){
        Vector3 characterHeightPosition = generatePositionWithOffset(transformToSet, 0, 1, 0);
        cameraPosition.transform.position = characterHeightPosition;
        float mouseX = (Input.mousePosition.x / Screen.width ) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
        cameraPosition.transform.rotation = Quaternion.Euler (new Vector4 (-1f * (mouseY * 180f), mouseX * 360f, transform.localRotation.z));
        

    }

    private void updateMousePosition(){
        lastMouse = Input.mousePosition - lastMouse ;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0 );
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse =  Input.mousePosition;
    }

    private Vector3 generatePositionWithOffset(Transform originalPosition, float xOffset, float yOffset, float zOffset){
        float x = originalPosition.transform.position.x + xOffset;
        float y = originalPosition.transform.position.y + yOffset;
        float z = originalPosition.transform.position.z + zOffset;
        return new Vector3(x,y,z);
    }

}
