using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Image sampleSprite;

    public Transform ParentPanel;

    public float inventoryWidth;
    public float inventoryHeight;
    public float maxInventoryItemsY;
    public float maxInventoryItemsX;
    private float inventoryTileHeight;
    private float inventoryTileWidth;

    private List<Image> inventoryItems;
    private int inventorySizeXAxis;
    private int inventorySizeYAxis;
    private List<List<Image>> inventoryMatrix;



    void Start()
    {
        inventoryHeight = 600;
        inventoryWidth = 700;
        maxInventoryItemsX = 10;
        maxInventoryItemsY = 10;
        inventoryTileHeight = inventoryHeight / maxInventoryItemsX;
        inventoryTileWidth = inventoryWidth / maxInventoryItemsY; 

        inventorySizeXAxis = 10;
        inventorySizeYAxis = 10;
        inventoryMatrix = new List<List<Image>>();
        for (int columnsOfInventory = 0; columnsOfInventory < inventorySizeXAxis; columnsOfInventory++ ){
            List<Image> row = new List<Image>();
            inventoryMatrix.Add(row);
        }

        List<GameObject> inventoryItems = searchForInventoryItemsInWorldSpace();
        List<Image> inventoryImages = obtainImageSpritesForInventoryItems(inventoryItems); 
        populateImageSpritesIntoInventoryPanel(inventoryImages);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<GameObject> searchForInventoryItemsInWorldSpace (){
        List <GameObject> inventoryItems = new List<GameObject>();
        return inventoryItems;
    }

    private List<Image> obtainImageSpritesForInventoryItems(List<GameObject> inventoryItems){
        List<Image> inventorySprites = new List<Image>();
        inventorySprites.Add(sampleSprite);
        return inventorySprites;
    }

    private void populateImageSpritesIntoInventoryPanel(List<Image> inventorySprites){
        int numberOfInventoryItems = inventorySprites.Count;
        foreach (Image image in inventorySprites){
            for (int inventoryRowX = 0; inventoryRowX < maxInventoryItemsX; inventoryRowX++){
                for (int inventoryColumnY = 0; inventoryColumnY < maxInventoryItemsY; inventoryColumnY++){
                    Debug.Log("AboutToAdd");
                    addItemToInventoryMatrix(image, inventoryColumnY, inventoryRowX);
                }
            }
           
        }
    }

    private GameObject generatePreFabAtSpawnPosition(){
        GameObject NewObj = new GameObject(); //Create the GameObject
        Image NewImage = NewObj.AddComponent<Image>();
        NewImage.sprite = sampleSprite.sprite;
        NewObj.GetComponent<RectTransform>().SetParent(ParentPanel.transform);
        NewObj.SetActive(true); //Activate the GameObject
        return NewObj;
    }

    
    public void addItemToInventoryMatrix(Image newInventoryItem, int yPosition, int xPosition){
        
        Debug.Log("XPosition is " + xPosition);
        Debug.Log("YPosition is " + yPosition);
       
        List<Image> inventoryColumn = inventoryMatrix[yPosition];
        Debug.Log("Count of Array is " + inventoryColumn.Count);
        if (inventoryColumn.Count < maxInventoryItemsX){
            generatePreFabAtSpawnPosition(newInventoryItem);
            inventoryColumn.Add(newInventoryItem);
        }
    } 

        private GameObject generatePreFabAtSpawnPosition(GameObject prefabToUse, Vector3  spawnPosition, Quaternion swingRotation){
        GameObject gameObjectPrefab = Instantiate(prefabToUse, spawnPosition, swingRotation);
        return gameObjectPrefab;
    }

        private GameObject generatePreFabAtSpawnPosition(Image imageToUse){
        GameObject NewObj = new GameObject(); //Create the GameObject
        Image NewImage = NewObj.AddComponent<Image>();
        NewImage.sprite = imageToUse.sprite;
        NewObj.GetComponent<RectTransform>().SetParent(ParentPanel.transform);
        NewObj.SetActive(true); //Activate the GameObject
        return NewObj;
    }

       private bool isInventoryRowMaxedOut(int rowToCheck){
        return false; 
    }

}
