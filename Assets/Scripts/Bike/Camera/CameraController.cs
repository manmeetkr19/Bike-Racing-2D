using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float initCameraSize;
    [SerializeField] private float desiredCamZoom = 8f;


    private Camera playerCam;
    private Rigidbody2D playerRb;
    private PlayerHealth playerH;


    void Start()
    {
        playerCam = GetComponent<Camera>();       
        initCameraSize = playerCam.orthographicSize;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRb = player.GetComponent<Rigidbody2D>();
        playerH = player.GetComponent<PlayerHealth>();

        //offset = transform.position - player.position;
    }

    private void Update()
    {
        if(playerH.Blasting == true)
        {
            //print("Returning..");
            return;
        }
        
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);

        if(playerRb.velocity.y > 3f ) // raising
        {
            //zoom out the camera
            playerCam.orthographicSize = Mathf.Lerp(playerCam.orthographicSize, desiredCamZoom, Time.deltaTime * 0.6f );         
        }
        else if(playerRb.velocity.y < -3f )
        {
            playerCam.orthographicSize = Mathf.Lerp(playerCam.orthographicSize, initCameraSize, Time.deltaTime * 0.6f);
        }  
    }

}
