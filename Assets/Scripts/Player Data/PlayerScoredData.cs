using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoredData : MonoBehaviour
{
    //STORES DISTANCE, COINS, SCORE, MONEY

    private float distanceFromDisk; //total distance covered till now
    private int coinsFromDisk; //total coins collected  till now
    private int scoreFromDisk;
    private int moneyFormDisk;

    private int levelXp = 0;

    private BikeController player;
    private PlayerHealth playerH;


    //Helps to control the iteration so coin does not saves again
    private bool distanceSaved = false;
    private bool coinSaved = false;
    private bool moneySaved = false;
    private bool scoreSaved = false;

    void Start()
    {
        player = FindObjectOfType<BikeController>();
        playerH = FindObjectOfType<PlayerHealth>();

        distanceFromDisk = PlayerPrefs.GetFloat("DistanceStoredAtRuntime");
        //print(distanceFromDisk);
        coinsFromDisk = PlayerPrefs.GetInt("CoinStoredAtRuntime");
        //print(coinsFromDisk);
        scoreFromDisk = PlayerPrefs.GetInt("ScoreStoredAtRuntime");

        moneyFormDisk = PlayerPrefs.GetInt("MoneyStoredAtRuntime");

        
    }

    public void SaveEssentialDataAtOnce()
    {
        //when exit or pause or any abrupt behaviour save data at once - Fail Safe
        SaveDistanceCovered();
        SaveCoinCollected();
        SaveMoneyCollected();
        SaveScoreCollected();

    }

    public void SaveDistanceCovered()
    {
        //can be called from anywhere 
        if (distanceSaved == true)
        {
            return;
        }

        // save the distance covered in bike controller
        distanceFromDisk += player.distanceCoveredInGame;
        PlayerPrefs.SetFloat("DistanceStoredAtRuntime", distanceFromDisk);
        distanceSaved = true;

        PlayerPrefs.Save();
    }

    public void SaveCoinCollected()
    {
        //can be called from anywhere
        if(coinSaved == true)
        {
            return;
        }

        coinsFromDisk += player.coinCount + playerH.bonusCoin; // bonus coin for airtime and blah blah
        PlayerPrefs.SetInt("CoinStoredAtRuntime", coinsFromDisk);
        coinSaved = true;

        PlayerPrefs.Save();
    }

    public void SaveMoneyCollected()
    {
        //can be called from anywhere
        if(moneySaved == true)
        {
            return;
        }

        moneyFormDisk += playerH.GetMoneyObtained();
        PlayerPrefs.SetInt("MoneyStoredAtRuntime", moneyFormDisk);
        moneySaved = true;

        PlayerPrefs.Save();
    }

    public void SaveScoreCollected()
    {
        //can be called from anywhere
        if (scoreSaved == true)
        {
            return;
        }

        scoreFromDisk += playerH.GetScoreObtained();
        PlayerPrefs.SetInt("ScoreStoredAtRuntime", scoreFromDisk);
        scoreSaved = true;

        PlayerPrefs.Save();
    }

    public float GetDistance()
    {
        return distanceFromDisk;
    }

    public int GetStarsCount()
    {
        return coinsFromDisk;
    }

    public int GetMoneyCount()
    {
        return moneyFormDisk;
    }

    public int GetScore()
    {
        return scoreFromDisk;
    }

    [ContextMenu("ResetAllDataFromGame")] //delete all data from game = context menue creates a right click option for this function
    private void ResetAllDataFromGame()
    {
        
        PlayerPrefs.DeleteAll();
    }


   


}
