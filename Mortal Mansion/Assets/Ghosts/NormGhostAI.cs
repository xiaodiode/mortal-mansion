using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormGhostAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent ghostAgent;
    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
		ghostAgent.updateRotation = false;
		ghostAgent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        ghostAgent.SetDestination(player.position);
    }
}
