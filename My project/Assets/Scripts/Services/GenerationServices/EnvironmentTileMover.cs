using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTileMover : MonoBehaviour
{
    public bool triggerTileSlide = false;

    public Vector3 positionToMoveTowards;
    float speed = 15;

    private float startTime = 0.01f;
    public float journeyLength;
    // Start is called before the first frame update
    void Start()
    {
       startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerTileSlide){
            transform.position = Vector3.MoveTowards(transform.position, positionToMoveTowards, 0.01f);
            if(transform.position == positionToMoveTowards){
                triggerTileSlide = true;
            }
        }
    }

    public void triggerTileMovement(){
        triggerTileSlide = true;
    }

    public void setPositionToMoveTowards(Vector3 transformOfPosition){
        positionToMoveTowards = transformOfPosition;
    }
}
