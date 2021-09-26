using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBomb : MonoBehaviour
{
    [SerializeField] private float force = 15f;
    [SerializeField] private float torque = 550f;
    [SerializeField] private GameObject ExplosionPrefab;

    private PlayerSoundManager sound;
    private CameraShake CamShake;

    private void Start()
    {
        sound = FindObjectOfType<PlayerSoundManager>();
        CamShake = GameObject.FindObjectOfType<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            //blast up
            Vibration.Vibrate(80);
            CamShake.StartScreenshake(0.7f, 0.5f);

            col.attachedRigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            col.attachedRigidbody.AddTorque(torque);

            GameObject blast = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            sound.PlayExplosion();
            Destroy(blast, 0.583f);

            Destroy(gameObject);
            
        }
        
    }
}
