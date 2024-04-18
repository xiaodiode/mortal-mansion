using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;

public class Floor : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public bool isBedroom;
    [SerializeField] public bool daytime;
    [SerializeField] public TilemapCollider2D floorCollider;
    [SerializeField] private Artifact artifact; 
    [SerializeField] private NormGhostAI ghost;
    [SerializeField] private GameObject hidingSpot;
    [SerializeField] private Bounds bounds;
    Vector3 randomPoint = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        bounds = floorCollider.bounds;

        artifact.setPosition(setRandomPosition(artifact.width, artifact.height, artifact.sprite));
        
        setGhost();

        // enableGhost(true);
        daytime = false;
        
        Debug.Log("the valid random point is: " + randomPoint);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(!daytime){
            if(collision.gameObject == player.gameObject){
                ghost.ghostActive = false;
                StartCoroutine(ghost.activate());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(ghost.ghostActive){
            if(collision.gameObject == player.gameObject){
                ghost.currState = normGhostState.Returning;
                StartCoroutine(ghost.deactivate());
            }
        }
    }

    public Vector3 setRandomPosition(float targetWidth, float targetHeight, GameObject sprite){
        
        randomPoint.x = Random.Range(bounds.min.x + targetWidth/2, bounds.max.x - targetWidth/2);
        randomPoint.y = Random.Range(bounds.min.y + targetHeight/2, bounds.max.y - targetHeight/2);

        Debug.Log("random point generated: " + randomPoint + " with x bounds between " + (bounds.min.x + targetWidth/2) + " and " + (bounds.max.x - targetWidth/2)
                       + " and y bounds between " + (bounds.min.y + targetHeight/2) + " and " + (bounds.max.y - targetHeight/2) + ": " + randomPoint);

        GameObject newObject = Instantiate(sprite, Vector3.zero, Quaternion.identity);
        newObject.transform.parent = transform;
        randomPoint.z = transform.position.z;
        newObject.transform.position = randomPoint;

        return randomPoint;
    }

    public void enableGhost(bool enable){
        if(enable){
            ghost.ghostActive = true;
            Debug.Log("attempt to set ghost active");
        }
    }

    private void setGhost(){

        // setting up ghost bounds
        Vector3 topLeft, topRight, botRight, botLeft;

        ghost.width = ghost.ghostCollider.bounds.size.x;
        ghost.height = ghost.ghostCollider.bounds.size.y;

        topLeft.x = bounds.min.x + ghost.width; topLeft.y = bounds.max.y - ghost.height; topLeft.z = transform.position.z;
        topRight.x = bounds.max.x - ghost.width; topRight.y = bounds.max.y - ghost.height; topRight.z = transform.position.z;
        botRight.x = bounds.max.x - ghost.width; botRight.y = bounds.min.y + ghost.height; botRight.z = transform.position.z;
        botLeft.x = bounds.min.x + ghost.width; botLeft.y = bounds.min.y + ghost.height; botLeft.z = transform.position.z;

        ghost.setWanderingPoints(topLeft, topRight, botRight, botLeft);

        ghost.setSpawnPosition(artifact.position);
    }

}
