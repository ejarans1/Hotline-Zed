using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTileManager : MonoBehaviour
{
    // Start is called before the first frame update

    private List<GameObject> inventoryItems;
    private int inventorySizeXAxis;
    private int inventorySizeYAxis;
    private List<List<GameObject>> inventoryMatrix;

    void Start()
    {
        inventorySizeXAxis = 10;
        inventorySizeYAxis = 10;
        inventoryMatrix = new List<List<GameObject>>();
        for (int loopCount = 0; loopCount < inventorySizeXAxis; loopCount++ ){
            List<GameObject> row = new List<GameObject>();
            inventoryMatrix.Add(row);
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void updateInventorySize(int yincrease, int xincrease){

    }

    public GameObject addItemToInventoryMatrix(GameObject newInventoryItem){

        return newInventoryItem;
    }

    private bool isInventoryRowMaxedOut(int rowToCheck){
        return false; 
    }

}
