using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandlerService : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Text counterText;

    public TMP_Text levelText;

    public ProceduralEnvironmentGenerationService proceduralEnvironmentGenerationService;

    public CharacterStatService characterStatService;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = proceduralEnvironmentGenerationService.getTileCount().ToString();
        levelText.text = characterStatService.getCurrentLevel().ToString();
        
    }
}
