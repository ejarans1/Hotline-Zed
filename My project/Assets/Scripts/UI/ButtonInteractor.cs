using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractor : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterStatService characterStatService;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void characterStatServiceCommand()
    {
        characterStatService.incrementHealthByAmount(10);
        Debug.Log(this.gameObject.name + ": In Button Interactor");
        Destroy(this.gameObject);
    }

    public void setCharacterStatService(CharacterStatService incomingCharacterStatService)
    {
        characterStatService = incomingCharacterStatService;
    }
}
