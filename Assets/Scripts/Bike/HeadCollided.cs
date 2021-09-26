using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollided : MonoBehaviour
{
    private PlayerHealth playerH;
    private CameraShake CamShake;

    private void Start()
    {
        playerH = transform.GetComponentInParent<PlayerHealth>();
        CamShake = GameObject.FindObjectOfType<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground") || collision.CompareTag("Hurdle") || collision.CompareTag("HeadCollision"))
        {
            //Head has hit something so we need to die - 
            playerH.OnHeadCollision(true); 
            CamShake.StartScreenshake(0.8f, 0.5f);
            Vibration.Vibrate(80);
        }
    }
}
