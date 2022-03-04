using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource enemyGrowlAudioSource;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playEnemyGrowlAudioSource()
    {
        if (!enemyGrowlAudioSource.isPlaying)
        {
            enemyGrowlAudioSource.Play();
        }
    }
    public void stopEnemyGrowlAudioSource()
    {
        enemyGrowlAudioSource.Pause();
    }


}
