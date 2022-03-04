using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerService : MonoBehaviour
{

    public CharacterStatService characterStatService; 
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterStatService.getCurrentHealth() == 0){
            string sceneName = "GAMEOVER";
            SceneManager.LoadScene(sceneName); 
        }
    }
}
