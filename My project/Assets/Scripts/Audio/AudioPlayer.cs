using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource objecTAudioSource;
    void Start()
    {
        triggerAudioSource();
    }

    // Update is called once per frame
    void Update()
    {
        //triggerAudioSource();
    }

    public void triggerAudioSource(){
        objecTAudioSource.Play();
    }
}
