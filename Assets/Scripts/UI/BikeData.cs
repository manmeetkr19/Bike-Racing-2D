using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="BikeData",menuName ="Vehicle/Bike")]
public class BikeData : ScriptableObject
{
    public int speed;
    public int acceleration;
    public int handling;
    public Sprite bikeImage;
    public GameObject bikePrefab;
}
