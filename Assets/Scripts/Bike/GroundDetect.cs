using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    private float LastAirTime = 0;
    private float AirTimeTotalInGame = 0f;

    public bool isGrounded;

    private PlayerHealth playerH;

    private void Start()
    {
        playerH = GetComponentInParent<PlayerHealth>();
        //Debug.LogWarning("StartDone - GroundDetect");
    }

    //if the collider is on trigger  -  grounded

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Ground") || col.CompareTag("Hurdle") )
        {
            isGrounded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Ground") || col.CompareTag("Hurdle"))
        {
            isGrounded = true;
        }
    }

    //not on triggered - not on ground
    //calculate artime for this state
    private void OnTriggerExit2D(Collider2D col)
    {
        
        if(col.CompareTag("Ground") || col.CompareTag("Hurdle"))
        {

            isGrounded = false;

        }
       
        
    }

    private void Update()
    {
        if(isGrounded == true  || playerH.IsAlive == false)
        {
            return;
        }

        if (Time.time > LastAirTime)
        {
            AirTimeTotalInGame += Time.deltaTime;
            LastAirTime = Time.time;
        }
        //Debug.Log(AirTimeTotalInGame);
    }

    public float GetTotalAirTime()
    {
        return AirTimeTotalInGame;
    }
}
