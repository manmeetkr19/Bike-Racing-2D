using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIhandler : MonoBehaviour
{
    [Header("UI Canvas References")]
    public GameObject startMenu;
    public GameObject settingMenu;
    public GameObject customizatioUI;
    public GameObject selectionUI;
    public GameObject[] uiToHideOnPlay;
    public GameObject iapCanvas;
    public GameObject pauseCanvas;
    public GameObject mainButtons;
    public GameObject settingButtons;
    public GameObject creditPanel;
    public GameObject exitCanvas;

    [Header("Rdeference of SFX and Music button")]
    public Sprite[] SfxSprite;
    public Sprite[] SoundSprite;
    public Image sfxButton;
    public Image musicButton;

    public int BikeSeelctionIndex;

   

    [Header("For stats in Scene 0 and 1")]

    [SerializeField] Text starText = null;
    [SerializeField] Text moneyText = null;
    [SerializeField] Text starText1 = null;
    [SerializeField] Text moneyText1 = null;

    private void Awake()
    {
        SfxImageChange();
        MusicButtonImage();
        //StatsViewer();
    }

    private void Start()
    {
        //Debug.Log(SceneManager.GetActiveScene().name);
       // StatsViewer();
    }

    public void SfxImageChange()
    {
        if (PlayerPrefs.GetString("PlaySfx")=="true")
        {
            sfxButton.sprite = SfxSprite[0];
        }
        else
            sfxButton.sprite = SfxSprite[1];
    }
    public void MusicButtonImage()
    {
        if (PlayerPrefs.GetString("PlayMusic") == "true")
        {
            musicButton.sprite = SoundSprite[0];
        }
        else
            musicButton.sprite = SoundSprite[1];
       // Debug.Log("accessed");
    }
   
    public void SettingfromMain()
    {
        startMenu.SetActive(false);
        settingMenu.SetActive(true);
        var settingAnim = GameObject.FindGameObjectWithTag("SettingPanelBG").GetComponent<Animator>(); //Adi was here
        settingAnim.SetTrigger("PlaySetting");
    }
    public void BackfromSeting()
    {
        Image settingPanel = GameObject.FindGameObjectWithTag("SettingPanelBG").GetComponent<Image>();
        var settingAnim = GameObject.FindGameObjectWithTag("SettingPanelBG").GetComponent<Animator>();// Adi was here 
        settingAnim.SetTrigger("GoToOriginal");

        if(settingPanel!=null)
            settingPanel.transform.localScale = new Vector3(0, 0, 0); 

        startMenu.SetActive(true);
        settingMenu.SetActive(false);
    }
    public void Start1()
    {
        SceneManager.LoadScene(BikeSeelctionIndex);
        Debug.Log(SceneManager.GetActiveScene().name);
        StatsViewer();
    }

    public void OpenBikeSelection()
    {
        SceneManager.LoadScene(1);
    }

    public void BakFromSelection()
    {
        SceneManager.LoadScene(0);
    }
    public void CustomtoSelect()
    {
        customizatioUI.SetActive(false);
        selectionUI.SetActive(true);
    }
    public void FinalBike()
    {
        for (int i = 0; i < uiToHideOnPlay.Length-1; i++)
        {
            uiToHideOnPlay[i].SetActive(false);
        }
        uiToHideOnPlay[2].SetActive(true);
    }
    
    public void StatsViewer()
    {
       
        if(starText!=null)
        {
            int coins = PlayerPrefs.GetInt("CoinStoredAtRuntime");
            starText.text = "" + coins;
        }
        if(moneyText!=null)
        {
            int money = PlayerPrefs.GetInt("MoneyStoredAtRuntime");
            moneyText.text = "" + money;
        }

        if(SceneManager.GetActiveScene().buildIndex==1)
        {
            if (starText1 != null)
            {
                int coins = PlayerPrefs.GetInt("CoinStoredAtRuntime");
                starText1.text = "" + coins;
            }
            if (moneyText1 != null)
            {
                int money = PlayerPrefs.GetInt("MoneyStoredAtRuntime");
                moneyText1.text = "" + money;
            }
        }
       
    }
    

    public void IAP()
    {
        iapCanvas.SetActive(true);
        startMenu.SetActive(false);
    }
    public void IAPClose()
    {
        iapCanvas.SetActive(false);
        startMenu.SetActive(true);
    }

    public void ExitCanvasActivate()
    {
        exitCanvas.SetActive(true);
    }

    public void DeactiveExitCanvas()
    {
        exitCanvas.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToBkFromSeason()
    {
        uiToHideOnPlay[2].SetActive(false);
        uiToHideOnPlay[0].SetActive(true);
    }

    public void Resume()
    {
        // var canvas = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<Canvas>();
        Time.timeScale = 1f;
        mainButtons.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void PauseGame()
    {
        // var canvas = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<GameObject>();
        Time.timeScale = 0f;
        mainButtons.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        string level = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(level);
    }

    public void SeasonChooser(int index)
    {
        if (index == 0)
            SceneManager.LoadScene(2);
        if (index == 1)
            SceneManager.LoadScene(3);
        if (index == 2)
            SceneManager.LoadScene(4);
    }
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ActivateCredit()
    {
        settingButtons.SetActive(false);
        creditPanel.SetActive(true);
    }

    public void DeactiveCredit()
    {
        creditPanel.SetActive(false);
        settingButtons.SetActive(true);
       
    }

    public void Instagram()
    {
        Application.OpenURL("https://www.instagram.com/magma_studios_/");
    }

    public void RatingStore()
    {

    }
}
