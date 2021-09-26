using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeSelector : MonoBehaviour
{
    public BikeData[] bikeData; // contains all the scriptable object of the bikes 
   // [SerializeField] private Image[] bikeImages = null;
   // [SerializeField] Image refToCustomCanvas = null;
    [SerializeField] private GameObject custzCanvas = null;
    [SerializeField] private GameObject selectionCanvas = null;
    BikeCustomization customization;
    UIhandler ui;
    public static int bike_id;
    void Start()
    {
        customization = FindObjectOfType<BikeCustomization>();
        ui = FindObjectOfType<UIhandler>();
        ui.StatsViewer();
      
    }

    void Update()
    {
        
    }
    public void BtnAction(int value)
    {
        bike_id = value;
        selectionCanvas.SetActive(false);
        custzCanvas.SetActive(true);
        // setImage();
        PlayerPrefs.SetInt("BikeIndex", bike_id); // using the index access the scriptable object array
        PlayerPrefs.Save();
        customization.dataSetter(bikeData[bike_id]);
       
    }
    void setImage()
    {
        //refToCustomCanvas.sprite = bikeImages[bike_id].sprite;
    }

    }

