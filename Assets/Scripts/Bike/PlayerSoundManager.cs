using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip GasRefill;
    [SerializeField] private AudioClip CoinPickup;
    [SerializeField] private AudioClip Explosion;
    [SerializeField] private AudioClip WoodBlockBreak;
    [SerializeField] private AudioClip EmptyFuel;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayGasRefill()
    {
        source.PlayOneShot(GasRefill, 1f);
    }

    public void PlayCoinPick()
    {
        source.PlayOneShot(CoinPickup, 1f);
    }

    public void PlayExplosion()
    {
        source.PlayOneShot(Explosion, 1f);
    }

    public void PlayWoodBlockBreak()
    {
        source.PlayOneShot(WoodBlockBreak, 1f);
    }


    public void FuelEmptySound()
    {
        source.PlayOneShot(EmptyFuel, 1f);
    }
}
