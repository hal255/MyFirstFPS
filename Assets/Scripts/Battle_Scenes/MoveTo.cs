using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

    public Transform goal; // destination

    void Start()
    {

        // get a reference to the NavMesh Agent component

        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        // set the agent in motion

        agent.destination = goal.position;

    }
}
