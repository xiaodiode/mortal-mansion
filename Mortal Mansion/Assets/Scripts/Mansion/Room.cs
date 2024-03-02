using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class Room : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public NavMeshModifier floorNavMesh;
    [SerializeField] public NavMeshModifier obstacleNavMesh;
    [SerializeField] public NavMeshModifier doorNavMesh;
    [SerializeField] public NavMeshAgent ghost;
    [SerializeField] public NavMeshSurface surface;

    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setupRoom(){
        // walkableNavMesh.
    }


}
