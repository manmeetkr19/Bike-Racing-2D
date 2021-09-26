using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public float power = 0.7f; //Strength of screenshake
    public float duration = 1.0f; //Duration of screenshake
    public Transform cam;
    public float slowDownAmount = 1.0f; //Time multiplier  
    public bool shouldShake = false; //if true - shake, false - dont 

    Vector2 startRotaiton;
    float initialDuration;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        startRotaiton = cam.localEulerAngles;
        initialDuration = duration;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldShake)
        {
            if(duration > 0)
            {
             cam.localEulerAngles = startRotaiton + (Vector2)Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                cam.localEulerAngles = startRotaiton;

            }
        }
        
    }

    public void StartScreenshake(float PowerOfShake, float DurationOfShake)
    {
         shouldShake = true;
         power = PowerOfShake;  
         initialDuration = DurationOfShake;
         duration = initialDuration;       
    }
}
