using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{
    [SerializeField] public bool isBedroom;
    [SerializeField] public TilemapCollider2D floorCollider;
    [SerializeField] private Artifact artifact; 
    [SerializeField] private NormGhostAI ghost;
    [SerializeField] private GameObject hidingSpot;
    Vector3 randomPoint = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        setTargetPosition(artifact.width, artifact.height, artifact.sprite);
        
        Debug.Log("the valid random point is: " + randomPoint);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        // if(collision.gameObject == player.gameObject){
            
        // }
    }

    public void setTargetPosition(float targetWidth, float targetHeight, GameObject sprite){
        Bounds bounds = floorCollider.bounds;

        randomPoint.x = Random.Range(bounds.min.x + targetWidth/2, bounds.max.x - targetWidth/2);
        randomPoint.y = Random.Range(bounds.min.y + targetHeight/2, bounds.max.y - targetHeight/2);

        Debug.Log("random point generated: " + randomPoint + " with x bounds between " + (bounds.min.x + targetWidth/2) + " and " + (bounds.max.x - targetWidth/2)
                       + " and y bounds between " + (bounds.min.y + targetHeight/2) + " and " + (bounds.max.y - targetHeight/2) + ": " + randomPoint);

        artifact.setPosition(randomPoint);

        GameObject newObject = Instantiate(sprite, Vector3.zero, Quaternion.identity);
        newObject.transform.parent = transform;
        randomPoint.z = transform.position.z;
        newObject.transform.position = randomPoint;
    }

}
