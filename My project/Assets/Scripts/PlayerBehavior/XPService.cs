using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPService : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public CharacterStatService characterStatService;
    float xpMeter;
    
    float xpMaxAmount;

    float increaseFactor;

    void Start()
    {
        xpMeter = 0;
        xpMaxAmount = 10;
        increaseFactor = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> experienceOrbs = collectExperienceOrbs();
        addOrbValues(experienceOrbs);
        calculatePotentialLevelIncrease();
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

    private void calculatePotentialLevelIncrease(){
        if (xpMeter > xpMaxAmount){
            xpMeter = 0;
            xpMaxAmount = xpMaxAmount * increaseFactor;
            characterStatService.increaseLevel();
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
