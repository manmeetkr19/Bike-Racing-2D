using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    private string coins1K = "com.magmastudios.motorbikeracing.1000coins";
    private string coins5K = "com.magmastudios.motorbikeracing.5000coins";
    private string money100 = "com.magmastudios.motorbikeracing.100money";

    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id==coins1K)
        {
            int coins = PlayerPrefs.GetInt("CoinStoredAtRuntime");
            coins += 1000;
            PlayerPrefs.SetInt("CoinStoredAtRuntime", coins);
            PlayerPrefs.Save();
            //Debug.Log("1K coin added");
        }

        if (product.definition.id == coins5K)
        {
            int coins = PlayerPrefs.GetInt("CoinStoredAtRuntime");
            coins += 5000;
            PlayerPrefs.SetInt("CoinStoredAtRuntime", coins);
            PlayerPrefs.Save();
            //Debug.Log("5K coin added");
        }

        if (product.definition.id == money100)
        {
            int money = PlayerPrefs.GetInt("MoneyStoredAtRuntime");
            money += 100;
            PlayerPrefs.SetInt("MoneyStoredAtRuntime",money);
            PlayerPrefs.Save();
           // Debug.Log("100 Monies added");
        }

    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(product.definition.id + " failed due to " + reason);
    }
}
