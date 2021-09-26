using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCan : MonoBehaviour
{
    public float GasFillAmount = 100f;
    private bool filledTheVehicle = false;

    private PlayerSoundManager sound;

    private void Start()
    {
        sound = FindObjectOfType<PlayerSoundManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        BikeController player = collision.transform.GetComponent<BikeController>();

        if (collision.CompareTag("Player") && filledTheVehicle == false)
        {
            player.RefillTheTank(true);

            filledTheVehicle = true; //So that fills only once

            sound.PlayGasRefill();
            Vibration.Vibrate(60);

            Destroy(gameObject);
        }
    }

}
