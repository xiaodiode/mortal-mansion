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

    [SerializeField] public bool artifactTouched;

    [SerializeField] private GameManager gameManager;


    private float verticalInput, horizontalInput;
    private Vector3 cameraPosition;

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

        artifactTouched = false;
    }

    // Update is called once per frame
    void Update()
    {
        move();

    }

    private void OnInteractArtifact(){
        if(artifactTouched){
            gameManager.artifactUI.toggleArtifact();
        }
    }
    private void OnNextArtifact(){
        gameManager.artifactUI.updateArtifact();
    }

    private void OnPauseGame(){
        Debug.Log("pausing game");
        gameManager.pauseGame(true);
    }

    public void OnResume(){
        gameManager.pauseGame(false);
    }

    private void move(){
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        movement.x = horizontalInput;
        movement.y = verticalInput;

        if(movement.magnitude > 1.0f){
            movement.Normalize();     
        }
        
        rb.velocity = new Vector2(movement.x*speedTotal, movement.y*speedTotal);
        
        // camera always focuses on player
        cameraPosition = transform.position;
        cameraPosition.z = cameraZ;
        playerCamera.transform.position = cameraPosition; 
        
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
