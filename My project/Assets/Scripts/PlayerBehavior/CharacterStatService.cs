using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatService : MonoBehaviour
{

    private int level;
    private float maxSkillPoints;
    private float maxHealthPoints;
    private float maxStamina;
    private float currentHealth;
    private float currentSkill;
    private float currentStamina;

    public Transform player; 
    public HealthBarService healthBarService;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;

        maxSkillPoints = 100;
        maxHealthPoints = 100;
        maxStamina = 100;

        currentHealth = 100;
        currentSkill = 100;
        currentStamina = 100;
    }

    public void decrementHealthByAmount(float amountToDecrement){
        if (currentStamina != 0) {
            currentHealth = currentHealth - amountToDecrement;
            healthBarService.setHealthSlider(currentHealth);
        }
    }

    public void decrementStaminaByAmount(float amountToDecrement){
        if (currentStamina != 0) {
            currentStamina = currentStamina - amountToDecrement;
        }
    }

    public void decrementCurrentSkill(float amountToDecrement){
        if (currentSkill != 0) {
            currentSkill = currentSkill - amountToDecrement;
        }
    }

    public void increaseLevel(){
        level++;
    }
    
    public float getCurrentHealth(){
        return currentHealth;
    }

    public int getCurrentLevel(){
        return level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
