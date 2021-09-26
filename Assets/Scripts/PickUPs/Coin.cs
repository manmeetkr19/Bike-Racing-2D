using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int pickUpAmount = 1;
    private bool coinPickedUp = false;

    private PlayerSoundManager sound;

    private void Start()
    {
        sound = FindObjectOfType<PlayerSoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        BikeController player = col.GetComponent<BikeController>();
        if (col.CompareTag("Player") && coinPickedUp == false)
        {
            player.CoinCollected(pickUpAmount);
            coinPickedUp = true; //So that fills only once

            sound.PlayCoinPick();

            Destroy(gameObject);
        }
    }

}
