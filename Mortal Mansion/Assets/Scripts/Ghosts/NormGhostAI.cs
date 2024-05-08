using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Transform player;

    [SerializeField] private NavMeshAgent ghostAgent;
    [SerializeField] public Collider2D ghostCollider;
    [SerializeField] public float width, height;
    [SerializeField] public GameObject ghostObject;
    [SerializeField] private SpriteRenderer ghostSprite;
    [SerializeField] public Vector3 spawnPosition;

    [SerializeField] private float activateDelay;
    [SerializeField] public bool ghostActive;
    [SerializeField] public bool inactiveReady;
    [SerializeField] public bool ghostDataReady;


    // top left, top right, bottom right, bottom left
    [SerializeField] public List<Vector3> wanderingBounds = new(); 
    [SerializeField] public List<string> wanderingTarget = new();
    [SerializeField] public string currWanderingTarget;
    [SerializeField] public int currWanderingIndex;

    [SerializeField] public normGhostState currState {get; set;}

    // private bool deactivating = false;

    // Start is called before the first frame update
    void Start()
    {
      ghostAgent.updateRotation = false;
      ghostAgent.updateUpAxis = false;

      currState = normGhostState.Idle;

      StartCoroutine(deactivate());

      // width = ghostCollider.bounds.size.x;
      // height = ghostCollider.bounds.size.y;

    }

    // Update is called once per frame
    void Update()
    {
      if(ghostActive){
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
    }

    public IEnumerator deactivate(){
      // deactivating = true;

      while(currState != normGhostState.Idle){
        yield return null;
      }

      ghostSprite.enabled = false;
      ghostActive = false;

      Debug.Log("deactivate complete");

      yield return new WaitForSeconds(5);

      Debug.Log("ghost is active now, will appear once room is entered");
    }

    public IEnumerator activate(){

      float elapsedTime = 0; 

      ghostSprite.enabled = true;

      while(elapsedTime <= activateDelay){

        elapsedTime += Time.deltaTime;

        yield return null;
      }

      currState = normGhostState.Wandering;
      ghostActive = true;
      
    }

    public void Idle(){
      inactiveReady = true;

    }

    public void Wander(){
      if(Mathf.Round(ghostAgent.transform.position.x) != Mathf.Round(wanderingBounds[currWanderingIndex].x)
            && Mathf.Round(ghostAgent.transform.position.y) != Mathf.Round(wanderingBounds[currWanderingIndex].y)){
        ghostAgent.SetDestination(wanderingBounds[currWanderingIndex]);
      }
      else{
        currWanderingIndex = Random.Range(0, wanderingBounds.Count);
        currWanderingTarget = wanderingTarget[currWanderingIndex];

        // Debug.Log("new target is: " + currWanderingTarget);
      }
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

    public void setWanderingPoints(List<Vector3> pointsList){
      wanderingBounds = pointsList;

      wanderingTarget.Add("top left");
      wanderingTarget.Add("top mid");
      wanderingTarget.Add("top right");

      wanderingTarget.Add("mid left");
      wanderingTarget.Add("mid mid");
      wanderingTarget.Add("mid right");

      wanderingTarget.Add("bottom left");
      wanderingTarget.Add("bottom mid");
      wanderingTarget.Add("bottom right");

      currWanderingIndex = Random.Range(0, wanderingBounds.Count);
      currWanderingTarget = wanderingTarget[currWanderingIndex];
    }

    public void setSpawnPosition(Vector3 position){
      spawnPosition = position;

      transform.position = spawnPosition;
    }

    void OnTriggerEnter2D(Collider2D collision){
      if(ghostActive){
        if(collision.gameObject == player.gameObject){
              // Debug.Log("ghost touched player");
          }  
      }
        
    }


}
