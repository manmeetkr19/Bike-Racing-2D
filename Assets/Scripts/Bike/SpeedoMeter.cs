using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedoMeter : MonoBehaviour
{
    private Image dial;

    [SerializeField] private Rigidbody2D rbOfBike;
    [SerializeField] private float maxSpeed = 100f;
    
    void Start()
    {
        rbOfBike = GameObject.FindGameObjectWithTag("BackTyre").GetComponent<Rigidbody2D>();
        dial = GetComponent<Image>();
    }

    void FixedUpdate()
    {
        dial.fillAmount = CalculatePercentage();
    }

    private float CalculatePercentage()
    {
        float percentage = (rbOfBike.velocity.magnitude * 100f) / maxSpeed;
        return percentage / 100f;
    }
}
