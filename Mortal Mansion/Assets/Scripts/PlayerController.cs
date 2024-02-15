using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public Camera camera;
    [SerializeField] public float speedBase;
    [SerializeField] public float visionBase;
    [SerializeField] public float sensingBase;


    [SerializeField] private Vector3 movement;
    [SerializeField] private float speedTotal;
    [SerializeField] private float visionTotal;
    [SerializeField] private float sensingTotal;


    private float verticalInput, horizontalInput;
    private Vector3 newPosition;

    private float cameraZ;


    // Start is called before the first frame update
    void Start()
    {
        movement = Vector3.zero;

        speedTotal = speedBase;

        newPosition = player.transform.position;

        cameraZ = camera.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    private void movePlayer(){
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        movement.x = horizontalInput*speedTotal*Time.deltaTime;
        movement.y = verticalInput*speedTotal*Time.deltaTime;

        newPosition = player.transform.position + movement; 
        player.transform.position = newPosition;

        newPosition.z = cameraZ;
        camera.transform.position = newPosition;
    }

    public void updateSpeed(float speedBoost){
        speedTotal = speedBase*speedBoost;
    }
}
