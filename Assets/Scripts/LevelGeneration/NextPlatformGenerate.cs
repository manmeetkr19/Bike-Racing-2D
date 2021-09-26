using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPlatformGenerate : MonoBehaviour
{
    private LevelGenerator lg;

    private void Start()
    {
        lg = FindObjectOfType<LevelGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            lg.GenerateNextPlatform();

            gameObject.SetActive(false); // prevent dual collision
        }

        
    }
}
