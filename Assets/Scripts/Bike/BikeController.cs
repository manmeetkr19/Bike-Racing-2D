using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeController : MonoBehaviour
{
    private float horizontal = 0f;
    private float vertical = 0f;


    [Header("BikeControls")]
    [SerializeField] private Rigidbody2D rb; //rb of whole bike
    [SerializeField] private Rigidbody2D rbFrontTyre;
    [SerializeField] private Rigidbody2D rbBackTyre;
    private GroundDetect gd;

    [SerializeField] private float speed = 40f;
    [SerializeField] private float balanceRotation = 1500f; // lower stable higher
    private PlayerHealth playerH;

    [Header("Fuel")]
    [SerializeField] private Image fuelBarrel;
    [SerializeField] private float maxFuel = 100f;    
    public float depletionRate = 0f; 
    public float currentFuel;
    private bool hasFuel = true;
    private bool refillTheGas = false;

    [Header("CoinCollection")]
    [SerializeField] private Text coinUiText;
    [HideInInspector]public int coinCount = 0; // updated during play

    [Header("DistanceCovered")]
    [SerializeField] private Text DistanceText;
    private Vector3 startPos = Vector3.zero;
    [HideInInspector]public float distanceCoveredInGame = 0; // value gets updated while play


    //Bike Sounds manager
    private Transform BikeSoundC;
    private Transform BikeAccelSound;

    private PlayerSoundManager soundP;
    bool FuelEmptyS = false;


    private void Start()
    {
        playerH = GetComponent<PlayerHealth>();
        gd = GetComponentInChildren<GroundDetect>();
        soundP = FindObjectOfType<PlayerSoundManager>();

        BikeSoundC = GameObject.FindGameObjectWithTag("BikeSounds").transform;
        BikeAccelSound = BikeSoundC.Find("Accel").transform;     
       
        currentFuel = maxFuel;
        startPos = transform.position;

        coinUiText = GameObject.FindGameObjectWithTag("CoinCountText").GetComponent<Text>();
        fuelBarrel = GameObject.FindGameObjectWithTag("FuelFill").GetComponent<Image>();
        DistanceText = GameObject.FindGameObjectWithTag("DistanceCountText").GetComponent<Text>();

        InvokeRepeating("CalculateDistance", 0f, 1 / speed); //call calculate distance function from 0 sec and once per 0.5 secs

        //Debug.LogWarning("Start Done - BikeController");
    }

    private void FixedUpdate()
    {

        //Differentiate between pc and android 
#if !UNITY_ANDROID && !UNITY_IPHONE && !UNITY_BLACKBERRY && !UNITY_WINRT || UNITY_EDITOR

        //Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        StartMoving(vertical);
        StartRotation(horizontal);
        HandleMotion();
        HandleBalancing();


#else
        //StartMoving(vertical);
        //StartRotation(horizontal);  //called by Ui button on android
        HandleMotion();
        HandleBalancing();
#endif


        //use the fuel when bike is moving
        UseFuel();

        //Sounds
        if(vertical == 0)
        {
            BikeAccelSound.gameObject.SetActive(false);
        }
        else if (vertical > 0)
        {
            BikeAccelSound.gameObject.SetActive(true);

        }
        
    }


    public void StartMoving(float vInput)
    {
        vertical = vInput;
        //print(vertical);
    }

    public void StartRotation(float hInput)
    {
        horizontal = hInput;
       // print(horizontal);

    }

    private void HandleMotion()
    {
        
        if(vertical == 0 || hasFuel == false || gd.isGrounded == false)
        {                   
            return;
        }      
        
        Vector2 motionForce = transform.right * vertical * speed;          
        float acceleration = motionForce.magnitude / rb.mass; 
        motionForce = Vector2.Lerp(motionForce, motionForce, acceleration);

        rbBackTyre.AddForce(motionForce  , ForceMode2D.Force);
        rbFrontTyre.AddForce(motionForce , ForceMode2D.Force);
        

        // clamp velocity
        Vector3 t_vel = rbBackTyre.velocity;
        t_vel.x = Mathf.Clamp(t_vel.x, -speed, speed);
        rbBackTyre.velocity = t_vel;
        rbFrontTyre.velocity = t_vel;

    }


    private void HandleBalancing()
    {
        if(horizontal == 0)
        {
            return;
        }
        rb.AddTorque(-horizontal * balanceRotation * Time.fixedDeltaTime);
    }


    private void UseFuel()
    {
        //Fuel over
        if (currentFuel <= 0)
        {
            if(hasFuel == false)
            {
                return;
            }
            hasFuel = false;
            Debug.LogError("Fuel Over");
            //wait some time and end the game if no fuel
            soundP.FuelEmptySound();
            StartCoroutine(WaitForFuel());
            
            return;
        }
        else
        {
            StopCoroutine(WaitForFuel());
            hasFuel = true;
        }

        //use up fuel as you move
        if (vertical != 0)
        {
            currentFuel -= depletionRate * Time.deltaTime;
            DepleteFuelUI();
        }

        
    }

    public void RefillTheTank(bool mode)
    {
        refillTheGas = mode;
        //Refill the gas when we hit any fuelCan
        if (refillTheGas == true)
        {
            currentFuel = 100f;
            
            refillTheGas = false;
            DepleteFuelUI();
        }
    }

    private void CalculateDistance()
    {
        if ( vertical < 0 || playerH.IsAlive == false) // if we are reversing/ dead in that case we dont want to calculate distance
        {
            return;
        }

        //max is used so if the bike falls and slides back then also the distance will not get reduced - preventing loss of score
        distanceCoveredInGame = Mathf.Max(  distanceCoveredInGame / 3f, (transform.position.x - startPos.x) / 3f     ) ;

        //  UNITS CONVERSION    
        if(distanceCoveredInGame < 1000)
        {
            //In Meters
            DistanceText.text = distanceCoveredInGame.ToString("#.0 mtr");
        }
        else if(distanceCoveredInGame >= 1000)
        {
            float dis = distanceCoveredInGame / 1000;
            DistanceText.text = dis.ToString("#.0 Km");
            
        }
    }

    private void DepleteFuelUI() //UI 
    {
        fuelBarrel.fillAmount = currentFuel / maxFuel;
                  
    }

    public void CoinCollected(int points) //UI
    {
        coinCount += points;
        coinUiText.text = coinCount.ToString();
    }

    private IEnumerator WaitForFuel()
    {
       
        yield return new WaitForSeconds(2f);
        //NoFuel
        //Die
        if(FuelEmptyS == false)
        {
            playerH.SetDataAfterDeath();
            FuelEmptyS = true;
        }
        

    }




}
