using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedBikeDetails : MonoBehaviour
{
    [SerializeField] Image speed = null;
    [SerializeField] Image acc = null;
    [SerializeField] Image hndling = null;
    [SerializeField] Image bikeImage = null;
    [SerializeField] GameObject buyCanvas=null;
    public Text priceText;
  //  private int selectedBikeIndex;
    private float maxSpeed, maxAcc, handling;
    public void dataSetter(BikeData data,int price)
    {
        maxSpeed = data.speed;
        maxAcc = data.acceleration;
        handling = data.handling;
        bikeImage.sprite = data.bikeImage;
       // priceText.text = "" + price;

        UpdateStats();
    }
    void UpdateStats()
    {

        speed.fillAmount = maxSpeed / 100;
        acc.fillAmount = maxAcc / 100;
        hndling.fillAmount = handling / 100;
    }

    public void CloseBuyCanvas()
    {
        buyCanvas.SetActive(false);
    }
}
