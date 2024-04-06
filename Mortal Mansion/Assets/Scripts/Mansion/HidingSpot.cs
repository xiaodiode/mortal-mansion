using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject sprite;
    [SerializeField] public Collider2D hidingSpotCollider;
    [SerializeField] public float width, height;
    [SerializeField] public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        width = hidingSpotCollider.bounds.size.x;
        height = hidingSpotCollider.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject == player){
            Debug.Log("hiding spot touched");
        }
    }

    public void setPosition(Vector3 newPosition){
        position = newPosition;
    }
}
