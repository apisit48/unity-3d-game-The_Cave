using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour {
    private Light myLight;
     
     
    void Start ()
    {
        myLight = GetComponent<Light>();
    }
     
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            myLight.enabled = !myLight.enabled;
        }
    }
}
