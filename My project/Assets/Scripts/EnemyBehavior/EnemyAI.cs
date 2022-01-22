using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
public Transform Player;
public float MoveSpeed = 4;
public float MaxDist = 10;
public float MinDist = 5;
 
 
 
 
 public void Start () 
 {
 
 }
 
 public void Update () 
 {
     transform.LookAt(Player);
     
     if(Vector3.Distance(transform.position,Player.position) >= MinDist && Vector3.Distance(transform.position,Player.position) <= MaxDist ){
     
          transform.position += transform.forward*MoveSpeed*Time.deltaTime;
 
           
           
          if(Vector3.Distance(transform.position,Player.position) <= MaxDist)
              {
            
    } 
    
    }
 }
}
