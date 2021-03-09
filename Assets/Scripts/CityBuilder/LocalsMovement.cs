using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LocalsMovement : MonoBehaviour
{
    public Board board;

    private NavMeshAgent navMeshAgent;
    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }
    private void RandomlyDestination()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator GoDestination()
    {
        while (true)
        {
            if (Vector3.Distance(destination, navMeshAgent.destination) > 1.0f)
            navMeshAgent.destination = destination;
            yield return null;
        }
        
    }


}
