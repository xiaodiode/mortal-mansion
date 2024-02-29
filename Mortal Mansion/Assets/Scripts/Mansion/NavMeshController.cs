using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshController : MonoBehaviour
{
    [SerializeField] public List<NavMeshSurface> navMeshSurfaces = new();
    [SerializeField] public List<Vector3> randomNavMeshPos = new();

    [SerializeField] private float randomPosRange;


    private Vector3 surfaceCenter;
    private NavMeshHit validPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public Vector3 findRandomPos(NavMeshSurface surface){
    //     surfaceCenter = surface.transform.position;

    //     if(NavMesh.SamplePosition(surfaceCenter, out validPosition, randomPosRange, NavMesh.AllAreas))
        

    //     return randomPos;
    // }
    
}
