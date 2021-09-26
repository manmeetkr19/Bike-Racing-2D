using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeUnlock : MonoBehaviour
{
    [SerializeField] GameObject overlayImage=null;
    [SerializeField] int price;

    bool isPurchased = false;

    [SerializeField] string key;

    private int coinsFromDisk;

    Button btn;
    //[SerializeField] Image[] bikeImages;

    public Text priceText;
    // public Image bikeShow;

    LockedBikeDetails details;

    public BikeData data;

    [SerializeField] GameObject buyCanvas=null;

    UIhandler uiManager;
    void Start()
    {
        if(PlayerPrefs.HasKey(key)==false)
        {
            PlayerPrefs.SetString(key, "false");
        }


        if (PlayerPrefs.GetString(key) == "true")
        {
            overlayImage.SetActive(false);
        }
        else
            overlayImage.SetActive(true);


        coinsFromDisk= PlayerPrefs.GetInt("CoinStoredAtRuntime");

        btn = GetComponent<Button>();
        details = FindObjectOfType<LockedBikeDetails>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString(key) == "false")
            btn.interactable = false;
        else
        {
            btn.interactable = true;
            EnableButton();
        }
            
    }

    private void EnableButton()
    {
        if (PlayerPrefs.GetString(key) == "true")
        {
            overlayImage.SetActive(false);
        }
    }

    public void PurchaseBike()
    {
        if (coinsFromDisk >= price)
        {
            coinsFromDisk -= price;
            PlayerPrefs.SetInt("CoinStoredAtRuntime", coinsFromDisk);
            PlayerPrefs.SetString(key, "true");
            EnableButton();
            uiManager = FindObjectOfType<UIhandler>();
            uiManager.StatsViewer();
        }
        else
            Debug.Log("Not Enough Money");
    }
    public void BuyCanvas()
    {
        details.dataSetter(data,price);
        buyCanvas.SetActive(true);
        //priceText.text =""+ price;
       
    }
    
}
