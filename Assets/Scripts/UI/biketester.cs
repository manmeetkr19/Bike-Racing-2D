using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biketester : MonoBehaviour
{
    // Start is called before the first frame update
    int index;
    void Start()
    {
        index = PlayerPrefs.GetInt("BikeIndex");
        Debug.Log(index);
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(index);
    }
}
