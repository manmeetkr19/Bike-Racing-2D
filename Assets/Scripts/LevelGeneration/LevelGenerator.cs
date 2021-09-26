using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private BikeController player;

    private GameObject BikeInGame;
    [SerializeField] private BikeData[] Bikes;
    public GameObject BikeExplosionPrefab;
    public GameObject HUDCanvas;
    public GameObject DeathCanvas;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject fuelPrefab;
    [Space(15)]
    [SerializeField] private GameObject[] LevelPrefabs;
    [Space(20)]
    [SerializeField] private GameObject[] hurdlePrefabs;
    [Space(15)]
    [SerializeField] private Transform StartGenerationFrom;

    private GameObject lastSpawnedPlatform;
    private GameObject currentSpawnedPlatform;
    private GameObject nextSpawnedPlatform;
    private GameObject lastToLastPlatform;

    


    private void Awake()
    {
        //At beginning we start with a platform
        currentSpawnedPlatform = Instantiate(LevelPrefabs[Random.Range(0, LevelPrefabs.Length)], StartGenerationFrom.position, Quaternion.identity);

    }
    private void Start()
    {
        BikeInGame = Bikes[PlayerPrefs.GetInt("BikeIndex")].bikePrefab;
        //Instantiate bike in the game
        Instantiate((BikeInGame), Vector3.zero, Quaternion.identity);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BikeController>();
        
    }

    public void GenerateNextPlatform()
    {
        InstantiatePlatform();  
    }

    private void InstantiatePlatform()
    {
        //when we hit the next trigger we have to spawn the next random platform and delete the last platform
        lastToLastPlatform = lastSpawnedPlatform;

        lastSpawnedPlatform = currentSpawnedPlatform;

        nextSpawnedPlatform = Instantiate(LevelPrefabs[Random.Range(0, LevelPrefabs.Length)], lastSpawnedPlatform.transform.Find("EndPoint").position, Quaternion.identity);
        
        GenerateHurdles(); //. new 
        GenerateCoin();
      
        GenerateFuel(); 

        currentSpawnedPlatform = nextSpawnedPlatform;

        DestructionOfOld();
    }

    private void GenerateHurdles()
    {
        //find the transforms with tag and instantiate a hurdle (Random)
        
        foreach (Transform t in nextSpawnedPlatform.transform.Find("HurdleHolder"))
        {
            Instantiate(hurdlePrefabs[Random.Range(0, hurdlePrefabs.Length)], t);
             //instantiate a hurdle

        }
    }

    private void GenerateCoin()
    {
        foreach (Transform t in nextSpawnedPlatform.transform.Find("CoinHolder"))
        {
            //OldCoinList.Add(Instantiate(coinPrefab, t.position, Quaternion.identity)); //instantiate a hurdle
            Instantiate(coinPrefab, t);

        }
    }

    private void GenerateFuel()
    {

        foreach (Transform t in nextSpawnedPlatform.transform.Find("FuelHolder"))
        {
            //OldFuelList.Add(Instantiate(coinPrefab, t.position, Quaternion.identity)); //instantiate a hurdle
            Instantiate(fuelPrefab, t);
            
        }
    }

    private void DestructionOfOld()
    {
        Destroy(lastToLastPlatform); //destroys the last platform just before us
       

    }

    //Called by UI buttons for motion and rotation 
    public void OrderToMove(int vInput)
    {
        player.StartMoving(vInput);
    }

    public void OrderToRotate(int hInput)
    {
        player.StartRotation(hInput);
    }


}
