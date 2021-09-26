using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestColor : MonoBehaviour
{
    [SerializeField] Image[] imageSelect=null;
    [SerializeField] Color[] colorsToFill = null;
    private Color currColor;
    private Image currImage;

    public void colorSelect(int index)
    {
        currColor = colorsToFill[index];
        currImage.color = currColor;
        
    }
    public void selectImage(int index)
    {
        currImage = imageSelect[index];
        Debug.Log(currImage.name);
    }

}
