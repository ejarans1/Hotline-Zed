using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabService : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getRandomPrefab(){
        List<GameObject> prefabsToChooseFrom = new List<GameObject>();
        int amountOfPrefab = gameObject.transform.childCount;
        for (int i = 1; i < amountOfPrefab; i++){
            prefabsToChooseFrom.Add(gameObject.transform.GetChild(i).gameObject);
        }
        return prefabsToChooseFrom[(Random.Range(0, this.transform.childCount))];

    }
}
