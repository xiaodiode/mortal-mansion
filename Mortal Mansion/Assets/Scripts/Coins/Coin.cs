using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CoinController coinController;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Coin Audio")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip coinSound; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        source.clip = coinSound;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject == player){
            coinController.coinCollected();
            source.Play();
            sprite.enabled = false;
            StartCoroutine(playSound());
        }
    }

    private IEnumerator playSound(){
        while(source.isPlaying){
            yield return null;
        }
        Destroy(gameObject);
        
    }

    
}
