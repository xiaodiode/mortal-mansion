using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.InputSystem;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Camera playerCamera;
    [SerializeField] public float speedBase;
    [SerializeField] public float visionBase;
    [SerializeField] public float sensingBase;


    [SerializeField] private Vector2 movement;
    [SerializeField] private float speedTotal;
    [SerializeField] private float visionTotal;
    [SerializeField] private float sensingTotal;


    private float verticalInput, horizontalInput;
    private Vector3 newPosition;

    private float cameraZ;

    private bool lockMovement;


    // Start is called before the first frame update
    void Start()
    {
        movement = Vector2.zero;

        speedTotal = speedBase;

        // newPosition = transform.position;

        cameraZ = playerCamera.transform.position.z;

        lockMovement = false;
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

        movement.Normalize(); 
        
        rb.velocity = new Vector2(movement.x, movement.y);
        // newPosition = transform.position;

        // newPosition.z = cameraZ;
        // playerCamera.transform.position = newPosition; 
        
        
    }

    public void updateSpeed(float speedBoost){
        speedTotal = speedBase*speedBoost;
    }

    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("collision with " +  collision);
        if(collision.gameObject.CompareTag("Obstacle")){
            lockMovement = true;
            Debug.Log("lockMovement: " + lockMovement);
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        lockMovement = false;
        Debug.Log("lockMovement: " + lockMovement);
    }
}
