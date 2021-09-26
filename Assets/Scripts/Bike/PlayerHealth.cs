using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private GameObject DeathCanvas;
    private GameObject HUDCanvas;
    private Transform ExplosionPoint;

    private Text ScoreTextUI;
    private Text CoinTextUI;
    private Text MoneyTextUI;
    private Text DistanceTextUI;
    private Text AirtimeTextUI;

    private Text ScoreHeaderText;
    private Text MoneyHeaderText;
    private Text StarHeaderText;

    private BikeController player;
    private GroundDetect GroundDetector;
    private PlayerScoredData playerScore;
    private LevelGenerator LG;
    private PlayerSoundManager sound;
    private PlayGames PGM;

    public int bonusCoin = 0;
    private int moneyCount = 0;
    private int scoreCount = 0;
    
    public bool IsAlive = true;
    public bool Blasting = false;
    private bool alreadyDone = false; // helper to stop multiple calls

   
    private void Start()
    {
        Time.timeScale = 1f;
        playerScore = FindObjectOfType<PlayerScoredData>();
        player = GetComponent<BikeController>();
        LG = GameObject.FindObjectOfType<LevelGenerator>();
        GroundDetector = GetComponentInChildren<GroundDetect>();
        sound = FindObjectOfType<PlayerSoundManager>();
        PGM = FindObjectOfType<PlayGames>();

        HUDCanvas = LG.HUDCanvas;
        DeathCanvas = LG.DeathCanvas;
        GetReference();       

        DeathCanvas.SetActive(false);
        HUDCanvas.SetActive(true);

        //Debug.LogWarning("StartDone - PlayerHealth");
    }

    private void GetReference()
    {
        ScoreTextUI = DeathCanvas.transform.Find("Panel/STATS/SCORE/ScoreText").GetComponent<Text>();
        CoinTextUI = DeathCanvas.transform.Find("Panel/STATS/STARS/StarsText").GetComponent<Text>();
        MoneyTextUI = DeathCanvas.transform.Find("Panel/STATS/MONEY/MoneyText").GetComponent<Text>();
        DistanceTextUI = DeathCanvas.transform.Find("Panel/STATS/DISTANCE/DistanceText").GetComponent<Text>();
        AirtimeTextUI = DeathCanvas.transform.Find("Panel/STATS/ADDITIONAL/AirtimeText").GetComponent<Text>();

        ScoreHeaderText = DeathCanvas.transform.Find("Panel/Header/ScoreH/ScoreText").GetComponent<Text>();
        MoneyHeaderText = DeathCanvas.transform.Find("Panel/Header/MoneyH/MoneyText").GetComponent<Text>();
        StarHeaderText = DeathCanvas.transform.Find("Panel/Header/StarH/StarText").GetComponent<Text>();

        //Debug.LogError("ReferenceDone - PlayerHealth");
    }

    public void OnHeadCollision(bool state)
    {
        if(state == false || alreadyDone == true)
        {
            return;
        }
        alreadyDone = true;
        Debug.Log("HeadCOllision");
        IsAlive = false;
                
        HUDCanvas.SetActive(false);
        StartCoroutine(WaitAfterDeath());        
    }

    //EndGame
    public void SetDataAfterDeath()
    {
        Time.timeScale = 0f;
        HUDCanvas.SetActive(false);
        DeathCanvas.SetActive(true);
        
        SetDeathCanvasValues();
    }

    private void SetDeathCanvasValues()
    {
        

        // ===AIRTIME====
        AirtimeTextUI.text =  GroundDetector.GetTotalAirTime().ToString("#.0 sec");
        bonusCoin += ((int)GroundDetector.GetTotalAirTime() / 50) * 25; // every 50 sec of airtime gives 25 coin
        

        //  ===DISTANCE===
        float distance = player.distanceCoveredInGame;
                //UNITS CONVERSION    
        if (distance < 1000)
        {
            //In Meters
            DistanceTextUI.text = distance.ToString("# mtr");
        }
        else if (distance >= 1000)
        {
            //In Kilometers
            DistanceTextUI.text = (distance/1000).ToString("#.0 Km");
        }

        bonusCoin += ((int)distance / 300) * 30; //every 300 meter gives 30 coin

        // ===COIN===
        CoinTextUI.text = (player.coinCount + bonusCoin).ToString();


        //  ===MONEY===
        moneyCount = (player.coinCount + bonusCoin) / 10;
        MoneyTextUI.text = moneyCount.ToString();

        //  ===SCORE===
        scoreCount = moneyCount + (int)distance;
        ScoreTextUI.text = scoreCount.ToString();

        //Update play games score - LEADERBOARD
        PGM.AddScoreToLeaderboard(scoreCount);

        //Save the data
        playerScore.SaveEssentialDataAtOnce();

        //Load values to header
        ScoreHeaderText.text = playerScore.GetScore().ToString();
        MoneyHeaderText.text = playerScore.GetMoneyCount().ToString();
        StarHeaderText.text = playerScore.GetStarsCount().ToString();

    }

    public int GetMoneyObtained()
    {
        return moneyCount;
    }

    public int GetScoreObtained()
    {
        return scoreCount;
    }

    private IEnumerator WaitAfterDeath()
    {
        yield return new WaitForSeconds(1f);
        DeathEffect();
        yield return new WaitForSeconds(2f);
        SetDataAfterDeath();

    }

    private void DeathEffect()
    {
        GameObject explosion = Instantiate(LG.BikeExplosionPrefab, gameObject.transform.Find("EXP").position, Quaternion.Euler(0, 0, 0));
        sound.PlayExplosion();

        Vibration.Vibrate(80);

        gameObject.transform.Find("GFX").gameObject.SetActive(false);
        Blasting = true;
        Destroy(explosion, 0.750f);
    }


}
