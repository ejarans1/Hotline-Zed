using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPService : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    float xpMeter;
    void Start()
    {
        xpMeter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> experienceOrbs = collectExperienceOrbs();
        addOrbValues(experienceOrbs);
        increaseXpInUi(xpMeter);
        deleteOrbs(experienceOrbs);
    }

    private List<GameObject> collectExperienceOrbs(){
        return searchGameSpaceForExperienceOrbs();
        
    }

    private List<GameObject> searchGameSpaceForExperienceOrbs(){
        List<GameObject> experienceOrbList = new List<GameObject>();
        GameObject[] taggedXP = GameObject.FindGameObjectsWithTag("XP");
        foreach (GameObject xpOrb in taggedXP){
            experienceOrbList.Add(xpOrb);
        }
        return experienceOrbList;
    }

    private void addOrbValues(List<GameObject> xpOrbsToAdd){
        foreach(GameObject xpOrb in xpOrbsToAdd){
            XpOrbData xpOrbData = xpOrb.GetComponent<XpOrbData>();
            float xpAmount = xpOrbData.getXpValue();
            xpMeter = xpMeter + xpAmount;
        }
    }

    private void deleteOrbs(List<GameObject> orbsToDelete){
        foreach(GameObject xpOrb in orbsToDelete){
            Destroy(xpOrb);
        }
    }

    public void increaseXpInUi(float newXPAmount){
        slider.value = newXPAmount;
    }
}
