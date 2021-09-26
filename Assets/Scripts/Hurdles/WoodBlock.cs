using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlock : MonoBehaviour
{
    private GameObject woodSprite;
    private GameObject woodAnim;    
    [SerializeField] private float force = 70f;
    [SerializeField] private float DestructionTime = 0.5f;

    private bool acted = false;
    
    private Rigidbody2D rb;
    private Animator anim;
    private CameraShake camShake;

    private void Start()
    {
        woodSprite = transform.Find("GFX/Sprite").gameObject; //find sprite GameObject
        woodAnim = transform.Find("GFX/Anim").gameObject; //find anim gameObject
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponentInChildren<Animator>(); //get anim component
        camShake = FindObjectOfType<CameraShake>();

        woodSprite.SetActive(true);
        woodAnim.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if(acted == true)
            {
                return;
            }
            
            Vibration.Vibrate(60); //vibrate the device = android
            camShake.StartScreenshake(0.7f, 0.5f);

            col.attachedRigidbody.AddForce(Vector2.right * -force, ForceMode2D.Impulse); //add backward force to reduce speed
                        
            acted = true;
            woodAnim.SetActive(true);
            woodSprite.SetActive(false);

            Destroy(gameObject, DestructionTime);
        }
        
    }
}
