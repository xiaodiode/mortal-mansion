using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CoinController coinController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject == player){
            coinController.coinCollected();
            Destroy(gameObject);
        }
    }

    
}
