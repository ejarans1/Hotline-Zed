using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class InventoryUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform parentPanel;
    public float inventoryWidth;
    public float inventoryHeight;
    public float maxInventoryItemsY;
    public float maxInventoryItemsX;
    public CharacterStatService characterStatService;


    private float xStartingPosition;
    private float yStartingPosition;

    private int inventorySizeXAxis;
    private int inventoryPopulatedYPosition;
    private int inventoryPopulatedXPosition;
    private List<List<Image>> inventoryMatrix;



    void Start() {
        initializeInventoryTrackerValues();
        populateInventoryColumns();
    }

    void Update() {
        toggleInventoryPanel();
    }

    public void populateSingleItemToInventory(Image imageSprite){
        checkCurrentInventoryRowAndColumnLimits();
        addItemToInventoryMatrix(imageSprite,inventoryPopulatedYPosition);
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

    public void addItemToInventoryMatrix(Image newInventoryItem, int yPosition){
        List<Image> inventoryColumn = inventoryMatrix[yPosition];
        if (inventoryColumn.Count < maxInventoryItemsX){
            generatePreFabAtSpawnPosition(newInventoryItem);
            inventoryColumn.Add(newInventoryItem);
        }
    } 

    private GameObject generatePreFabAtSpawnPosition(Image imageToUse) {
        GameObject inventoryItem = initializeInventoryItemWithImageAndButton(imageToUse);
        setPositionOfItemInInventory(inventoryItem);
        enableInventoryItem(inventoryItem);
        return inventoryItem;
    }

    private GameObject initializeInventoryItemWithImageAndButton(Image imageToUse)
    {
        GameObject inventoryItem = new GameObject();
        Button newButton = configureButtonForInventoryInteraction(inventoryItem);
        Image newImage = inventoryItem.AddComponent<Image>();
        newButton.targetGraphic = newImage;
        newImage.sprite = imageToUse.sprite;
        return inventoryItem;
    }

    private  Button configureButtonForInventoryInteraction(GameObject newObj)
    {
        Button newButton = newObj.AddComponent<Button>();
        ColorBlock colorVar = newButton.colors;
        colorVar.highlightedColor = new Color(91, 192, 41);
        newButton.colors = colorVar;
        newButton.onClick.AddListener(delegate { characterStatService.incrementHealthByAmount(10); });
        return newButton;
    }

    private void setPositionOfItemInInventory(GameObject newObj) {
        newObj.GetComponent<RectTransform>().SetParent(parentPanel.transform);
        Vector3 topRight = getTopRightCorner();
        newObj.transform.localPosition = generatePositionWithOffset(topRight);
    }

    private static void enableInventoryItem(GameObject newObj) {
        newObj.SetActive(true);
    }

    private Vector3 generatePositionWithOffset(Vector3 originalPosition) {
        incrementInventoryTrackers();
        Vector3 nextInventoryPosition = generateCurrentInventoryPosition(originalPosition);
        return nextInventoryPosition;
    }

    private Vector3 generateCurrentInventoryPosition(Vector3 originalPosition) {
        float x = originalPosition.x + xStartingPosition * 100 + 50;
        float y = originalPosition.y + yStartingPosition * 100 + 50;
        float z = 0;
        return new Vector3(x, y, z);
    }

    private void incrementInventoryTrackers(){
        if (yStartingPosition == 9){
            Debug.Log("Last Y Position");    
        }
        if (xStartingPosition == 9){
            xStartingPosition = -1;
            yStartingPosition = yStartingPosition + 1;
        }
        xStartingPosition++;
    
    }

    private void initializeInventoryTrackerValues() {
        inventoryHeight = 600;
        inventoryWidth = 700;

        maxInventoryItemsX = 10;
        maxInventoryItemsY = 10;

        inventorySizeXAxis = 10;

        inventoryPopulatedYPosition = 0;
        inventoryPopulatedXPosition = 0;

        xStartingPosition = -1;
        yStartingPosition = 0;
    }
    private void populateInventoryColumns() {
        inventoryMatrix = new List<List<Image>>();
        for (int columnsOfInventory = 0; columnsOfInventory < inventorySizeXAxis; columnsOfInventory++) {
            List<Image> row = new List<Image>();
            inventoryMatrix.Add(row);
        }
    }

    private void toggleInventoryPanel() {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            parentPanel.gameObject.SetActive(!parentPanel.gameObject.active);
        }
    }

    public Vector3 getTopRightCorner() {
        Vector3[] v = new Vector3[4];
        parentPanel.GetLocalCorners(v);
        return v[0];

    }

}
