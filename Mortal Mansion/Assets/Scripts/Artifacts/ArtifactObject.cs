using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ArtifactObject : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public GameObject artifactObject;
    [SerializeField] public Collider2D artifactCollider;
    [SerializeField] public float width, height;
    [SerializeField] public Vector3 position;
    [SerializeField] public bool artifactReady;

    // Start is called before the first frame update
    void Start()
    {
        artifactReady = false; 

        width = artifactCollider.bounds.size.x;
        height = artifactCollider.bounds.size.y;

        artifactReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject == player.gameObject){
            Debug.Log("artifact touched");
            player.artifactTouched = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject == player.gameObject){
            Debug.Log("artifact out of range");
            player.artifactTouched = false;
        }
    }

    public void setPosition(Vector3 newPosition){
        position = newPosition;

        transform.position = position;
    }
}
