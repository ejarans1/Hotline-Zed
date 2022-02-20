using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Image sampleSprite;
    public RectTransform ParentPanel;
    public float inventoryWidth;
    public float inventoryHeight;
    public float maxInventoryItemsY;
    public float maxInventoryItemsX;
    private float inventoryTileHeight;
    private float inventoryTileWidth;

    private float xStartingPosition;
    private float yStartingPosition;

    private List<Image> inventoryItems;
    private int inventorySizeXAxis;
    private int inventorySizeYAxis;
    private int inventoryPopulatedYPosition;
    private int inventoryPopulatedXPosition;
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

        inventoryPopulatedYPosition = 0;
        inventoryPopulatedXPosition = 0;

        xStartingPosition = -1;
        yStartingPosition = 0;



        inventoryMatrix = new List<List<Image>>();

        for (int columnsOfInventory = 0; columnsOfInventory < inventorySizeXAxis; columnsOfInventory++ ){
            List<Image> row = new List<Image>();
            inventoryMatrix.Add(row);
        }

        List<GameObject> inventoryItems = searchForInventoryItemsInWorldSpace();
        List<Image> inventoryImages = obtainImageSpritesForInventoryItems(inventoryItems); 
        //populateImageSpritesIntoInventoryPanel(inventoryImages);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        //populateSingleItemToInventory(sampleSprite);
    }

    public Vector3 getTopRightCorner()
    {
        Vector3[] v = new Vector3[4];
        ParentPanel.GetLocalCorners(v);
        return v[0];

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

    public void populateSingleItemToInventory(Image imageSprite){
        checkCurrentInventoryRowAndColumnLimits();
        addItemToInventoryMatrix(imageSprite,inventoryPopulatedYPosition, inventoryPopulatedXPosition);
        inventoryPopulatedXPosition++;
    }

    private void checkCurrentInventoryRowAndColumnLimits(){
        if(inventoryPopulatedXPosition == maxInventoryItemsX){
            inventoryPopulatedXPosition = 0;
            inventoryPopulatedYPosition = inventoryPopulatedYPosition + 1;
        }
        if(inventoryPopulatedYPosition == maxInventoryItemsY){
            inventoryPopulatedYPosition = 0;
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
        List<Image> inventoryColumn = inventoryMatrix[yPosition];
        if (inventoryColumn.Count < maxInventoryItemsX){
            generatePreFabAtSpawnPosition(newInventoryItem, xPosition, yPosition);
            inventoryColumn.Add(newInventoryItem);
        }
        } 

        private GameObject generatePreFabAtSpawnPosition(GameObject prefabToUse, Vector3  spawnPosition, Quaternion swingRotation){
        GameObject gameObjectPrefab = Instantiate(prefabToUse, spawnPosition, swingRotation);
        return gameObjectPrefab;
    }

        private GameObject generatePreFabAtSpawnPosition(Image imageToUse, float xPosition, float yPosition){
            GameObject NewObj = new GameObject(); //Create the GameObject
            Image NewImage = NewObj.AddComponent<Image>();
            float xOffset = calculateImageXPosition(xPosition);
            float yOffset = calculateImageYPosition(yPosition);
            NewImage.sprite = imageToUse.sprite;
            NewObj.GetComponent<RectTransform>().SetParent(ParentPanel.transform);
            Vector3 topRight = getTopRightCorner();
            Debug.Log(topRight.ToString());
            NewObj.transform.localPosition = generatePositionWizthOffset(topRight, xOffset, yOffset, 10);

            NewObj.SetActive(true); //Activate the GameObject
            return NewObj;
    }

       private bool isInventoryRowMaxedOut(int rowToCheck){
        return false; 
    }

    private float calculateImageXPosition(float xPosition){
        return xPosition * 100; 
    }

    private float calculateImageYPosition(float yPosition){
        return yPosition * 100; 
    }

    private void generateNextPosition(){
        if (yStartingPosition == 9){
            Debug.Log("Last Y Position");    
        }
        if (xStartingPosition == 9){
            xStartingPosition = -1;
            yStartingPosition = yStartingPosition + 1;
        }
        xStartingPosition++;
    
    }


    private Vector3 generatePositionWizthOffset(Vector3 originalPosition, float xOffset, float yOffset, float zOffset){
        generateNextPosition();
        float x = originalPosition.x + xStartingPosition * 100 + 50;
        float y = originalPosition.y + yStartingPosition * 100 + 50;
        float z = 0;
        return new Vector3(x,y,z);
    }

}
