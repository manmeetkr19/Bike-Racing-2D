using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenAnim : MonoBehaviour
{
    public RectTransform menuContainer;
    private Vector3 desiredMenuPosition;
    public float timeToChange = 1f; //Time after which the slide should change
    private float timeTemp = 0f;
    private int loopCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        menuContainer = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeTemp + timeToChange)
        {
            //Loop between values 0 1 2 0 1 2 0 1 2
            NavigateTo(loopCounter);
            loopCounter++;
            
            if(loopCounter == 3)
            {
                loopCounter = 0;
            }

            timeTemp = Time.time;

        }
        
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.05f);
    }

    private void NavigateTo(int menueIndex) // 0 - main, 1 - play, 2 - shop
    {
        switch(menueIndex)
        {
            //default and 0 is Winter
            default:
            case 0:
                desiredMenuPosition = Vector3.zero;
                break;

            //1 is Halloween
            case 1:
                desiredMenuPosition = Vector3.up * 1080;
                break;

            case 2:
                desiredMenuPosition = Vector3.up * -1080;
                break;
        }

        
    }
}
