using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum normGhostState{
  Idle,
  Wandering,
  Following,
  Returning
}

public class NormGhostAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent ghostAgent;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] public bool enableGhost;

    [SerializeField] public normGhostState currState {get; set;}

    // Start is called before the first frame update
    void Start()
    {
      ghostAgent.updateRotation = false;
      ghostAgent.updateUpAxis = false;

    }

    // Update is called once per frame
    void Update()
    {

      switch(currState){
        case normGhostState.Idle:
          Idle();
          break;

        case normGhostState.Wandering:
          Wander();
          break;

        case normGhostState.Following:
          Follow();
          break;

        case normGhostState.Returning:
          Return();
          break;
      }

      
    }

    public void Idle(){
      enableGhost = false;
    }

    public void Wander(){
      enableGhost = true;
    }

    public void Follow(){
      ghostAgent.SetDestination(player.position);
    }

    public void Return(){
      if(ghostAgent.transform.position != spawnPosition){
        ghostAgent.SetDestination(spawnPosition);
      }
      else{
        currState = normGhostState.Idle;
      }
    }
}
