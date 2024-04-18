using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRadius : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public NormGhostAI ghost;
    [SerializeField] private RectTransform detectionTransform;
    [SerializeField] private float detectionSize;

    
    // Start is called before the first frame update
    void Start()
    {
        setupDetection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setupDetection(){
        Vector3 detectionScale = new Vector3(detectionSize, detectionSize, detectionSize);
        detectionTransform.localScale = detectionScale;
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(ghost.ghostActive){
            if(collision.gameObject == player.gameObject){
                ghost.currState = normGhostState.Following;
                Debug.Log("entered trigger radius, now following");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(ghost.ghostActive){
            if(collision.gameObject == player.gameObject){
                ghost.currState = normGhostState.Wandering;
            }
        }
    }
    
}
