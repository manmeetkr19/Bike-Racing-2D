using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static bool canPlayMusic = true;
    private bool playSfx=true;
    private bool playMusic=true;
    private AudioSource sfx;
    private AudioSource music;

    private UIhandler uiManager;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("PlaySfx")==false)
        {
            PlayerPrefs.SetString("PlaySfx", "true");
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey("PlayMusic") == false)
        {
            PlayerPrefs.SetString("PlayMusic", "true");
            PlayerPrefs.Save();
        }
    }


    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        music = GameObject.FindGameObjectWithTag("MUSIC").GetComponent<AudioSource>();
        ButtonMusic();

        if (PlayerPrefs.GetString("PlaySfx") == "true")
        {
            playSfx = true;
        }
        else playSfx = false;

        if (PlayerPrefs.GetString("PlayMusic") == "true")
        {
            playMusic = true;
        }
        else
            playMusic = false;

        uiManager = FindObjectOfType<UIhandler>();
    }

    public void ButtonSfx()
    {
        if(PlayerPrefs.GetString("PlaySfx")=="true")
        {
            sfx.Play();
        }
    }
    public void ButtonMusic()
    {
        if (PlayerPrefs.GetString("PlayMusic") == "true")
        {
            music.Play();
        }
        else
            music.Stop();
       // Debug.Log(playMusic);
    }
    public void PlaySfx()
    {
        if (playSfx == true)
        {
            playSfx = false;
            PlayerPrefs.SetString("PlaySfx", "false");
        }
        else if (playSfx == false)
        {
            playSfx = true;
            PlayerPrefs.SetString("PlaySfx", "true");
        }
        PlayerPrefs.Save();
        uiManager.SfxImageChange();
        ButtonSfx();
        
        Debug.Log("pressed and value is " + PlayerPrefs.GetString("PlaySfx"));

    }

    
    public void PlaySound()
    {
        if (playMusic == true)
        {
            playMusic = false;
            PlayerPrefs.SetString("PlayMusic", "false");
        }            
        else if (playMusic == false)
        {
            playMusic = true;
            PlayerPrefs.SetString("PlayMusic", "true");
        }
        //Debug.Log(playMusic);
        PlayerPrefs.Save();
        uiManager.MusicButtonImage();
        ButtonMusic();
    }

}
