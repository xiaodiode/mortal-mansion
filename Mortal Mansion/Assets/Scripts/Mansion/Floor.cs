using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;

public class Floor : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public bool isBedroom;
    [SerializeField] public bool daytime;
    [SerializeField] public TilemapCollider2D floorCollider;
    [SerializeField] private ArtifactObject artifact; 
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

    private void setGhost(){
        // finding dimensions of ghost to prevent it from walking out of bounds
        ghost.width = ghost.ghostCollider.bounds.size.x;
        ghost.height = ghost.ghostCollider.bounds.size.y;

        // setting up ghost bounds
        Vector3 topLeft, topMid, topRight, 
            midLeft, midMid, midRight, 
            botLeft, botMid, botRight;

        float Xmin, Xmax, Xcenter, 
            Ymin, Ymax, Ycenter;

        Xmin = bounds.min.x + ghost.width;
        Xmax = bounds.max.x - ghost.width;
        Xcenter = (Xmax - Xmin)/2 + Xmin;

        Ymin = bounds.min.y + ghost.height;
        Ymax = bounds.max.y - ghost.height;
        Ycenter = (Ymax - Ymin)/2 + Ymin;

        topLeft.x = Xmin; topLeft.y = Ymax; topLeft.z = transform.position.z;
        topMid.x = Xcenter; topMid.y = Ymax; topMid.z = transform.position.z;
        topRight.x = Xmax; topRight.y = Ymax; topRight.z = transform.position.z;

        midLeft.x = Xmin; midLeft.y = Ycenter; midLeft.z = transform.position.z;
        midMid.x = Xcenter; midMid.y = Ycenter; midMid.z = transform.position.z;
        midRight.x = Xmax; midRight.y = Ycenter; midRight.z = transform.position.z;

        botLeft.x = Xmin; botLeft.y = Ymin; botLeft.z = transform.position.z;
        botMid.x = Xcenter; botMid.y = Ymin; botMid.z = transform.position.z;
        botRight.x = Xmax; botRight.y = Ymin; botRight.z = transform.position.z;

        List<Vector3> wanderingPoints = new()
        {
            topLeft,
            topMid,
            topRight,
            midLeft,
            midMid,
            midRight,
            botLeft,
            botMid,
            botRight
        };
        

        ghost.setWanderingPoints(wanderingPoints);

        ghost.setSpawnPosition(artifact.position);
    }

}
