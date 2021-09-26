using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonUnlock : MonoBehaviour
{
    [SerializeField] GameObject overlayImage = null;
    [SerializeField] int price;
    bool isPurchased = false;
    [SerializeField] string key;
    [SerializeField] Image border = null;
   
    private int moneyFormDisk;
    Button btn;
   
    public Text priceText;
    private UIhandler uiManager;
    
    void Start()
    {
        if (PlayerPrefs.HasKey(key) == false)
        {
            PlayerPrefs.SetString(key, "false");
        }


        if (PlayerPrefs.GetString(key) == "true")
        {
            overlayImage.SetActive(false);
        }
        else
            overlayImage.SetActive(true);


        moneyFormDisk = PlayerPrefs.GetInt("MoneyStoredAtRuntime");

        btn = GetComponent<Button>();
       
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
        var color = border.color;
        color.a = 1;
        border.color = color;

        if (PlayerPrefs.GetString(key) == "true")
        {
            overlayImage.SetActive(false);
        }
    }

    public void UnlockSeason()
    {
        if (moneyFormDisk >= price)
        {
            moneyFormDisk -= price;
            PlayerPrefs.SetInt("CoinStoredAtRuntime", moneyFormDisk);
            PlayerPrefs.SetString(key, "true");
            EnableButton();
            uiManager = FindObjectOfType<UIhandler>();
            uiManager.StatsViewer();
        }
        else
            Debug.Log("Not Enough Money");
    }
   
}
