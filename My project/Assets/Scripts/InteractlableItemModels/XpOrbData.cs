using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpOrbData : MonoBehaviour
{
    // Start is called before the first frame update

    float xpValue;
    void Start()
    {
        xpValue = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getXpValue(){
        return xpValue;
    }
}
