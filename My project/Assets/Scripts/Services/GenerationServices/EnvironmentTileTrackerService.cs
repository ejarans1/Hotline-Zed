using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTileTrackerService : MonoBehaviour
{


    private List<List<bool>> tileMatrix;

    private int currentXTilePosition;

    private int currentYTilePosition;

    // Start is called before the first frame update
    void Start()
    {
       currentXTilePosition = 50;
       currentYTilePosition = 50;
       tileMatrix = new List<List<bool>>();
       prePopulateTiles(100, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getMatrixValue(int xAxis, int yAxis){
        return tileMatrix[xAxis][yAxis];
    }

    public bool setMatrixValue(int xAxis, int yAxis){
        return tileMatrix[xAxis][yAxis];
    }


    public int getCurrentXTilePosition(){
        return currentXTilePosition;
    }
    public int getCurrentYTilePosition(){
        return currentYTilePosition;
    }

    public void modifyCurrentYTilePosition(int currentYPosition){
        currentYTilePosition = currentYPosition;
    }

    public void modifyCurrentXTilePosition(int currentXPosition){
        currentXTilePosition = currentXPosition;

    }

    private void prePopulateTiles(int xRange, int yRange){
        for (int i = 0; i < xRange; i++){   
            List<bool> column = new List<bool>();  
            for (int z = 0; z < yRange; z++){
                column.Add(false);
            }
            tileMatrix.Add(column);
        }        
    }

}
