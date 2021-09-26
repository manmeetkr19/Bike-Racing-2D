using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeCustomization : MonoBehaviour
{
    [SerializeField] Image speed=null;
    [SerializeField] Image acc=null;
    [SerializeField] Image hndling=null;
    [SerializeField] Image bikeImage = null;
    private int selectedBikeIndex;
    private float maxSpeed, maxAcc, handling;
    public void dataSetter(BikeData data)
    {
        maxSpeed = data.speed;
        maxAcc = data.acceleration;
        handling = data.handling;
        bikeImage.sprite = data.bikeImage;

        UpdateStats();
    }
    void UpdateStats()
    {
        
        speed.fillAmount =maxSpeed/100;
        acc.fillAmount = maxAcc / 100;
        hndling.fillAmount = handling / 100;
    }
}
