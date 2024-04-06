using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{
    [SerializeField] public TilemapCollider2D floorCollider;
    [SerializeField] public GameObject placeholder; 
    Vector3 randomPoint = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("the valid random point is: " + getRandomPoint());
        // createObjectAtPoint();
    }

    void Awake(){
        Debug.Log("the valid random point is: " + getRandomPoint());
        createObjectAtPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        // if(collision.gameObject == player.gameObject){
            
        // }
    }

    public Vector3 getRandomPoint(){
        Bounds bounds = floorCollider.bounds;
        bool validPoint = false;

        Collider2D hitCollider;

        // while(!validPoint){
            randomPoint.x = Random.Range(bounds.min.x, bounds.max.x);
            randomPoint.y = Random.Range(bounds.min.y, bounds.max.y);

            // Debug.Log("random point generated: " + randomPoint + " with x bounds between " + bounds.min.x + " and " + bounds.max.x
            //            + " and y bounds between " + bounds.min.y + " and " + bounds.max.y + ": " + randomPoint);
        
            // hitCollider = Physics2D.OverlapPoint(randomPoint, floorCollider.gameObject.layer);

            // if(hitCollider != null && hitCollider == floorCollider){
            //     validPoint = true;
            // }

            // else{
            //     validPoint = false;
            // }

            // Debug.Log("valid point? " + validPoint);

        // }

        return randomPoint;
        
    }

    public void createObjectAtPoint(){
        GameObject newObject = Instantiate(placeholder, Vector3.zero, Quaternion.identity);
        newObject.transform.parent = transform;
        randomPoint.z = transform.position.z;
        newObject.transform.position = randomPoint;
    }
}
