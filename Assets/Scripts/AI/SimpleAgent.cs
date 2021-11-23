using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SimpleAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private float mobilityRange = 10;

    [SerializeField] private float minWaitTime = 1;
    [SerializeField] private float maxWaitTime = 4;

    [Header("Wander")][Space]
    [SerializeField] private float wanderRadius = 10;
    [SerializeField] private float wanderDistance = 20;
    [SerializeField] private float wanderJitter = 1;

    private float wanderingTime = 0;

    private Vector3 wanderTarget = Vector3.zero;
    private bool wanderingResetInvoked = false;



    [SerializeField] private bool debug;

    Vector3 initialPosition = new Vector3();

    private float timePassed = 0;

    // Start is called before the first frame update
    private void OnEnable()
    {
        if(navAgent == null)
            navAgent = transform.GetComponent<NavMeshAgent>();

        initialPosition = transform.position;
    }
    void Start()
    {
        wanderingTime = Random.Range(5, 10);
        //Invoke(nameof(SetRandomDestination), 0f);   
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed < wanderingTime) 
        { 
            Wander(); 
        }
        else if (!wanderingResetInvoked)
        {
            Invoke(nameof(RandomResetWanderingTime), Random.Range(1, 9));
            wanderingResetInvoked = navAgent.isStopped = true;
        }
    }
    private void OnDrawGizmos()
    {
        if (debug) 
        {
            Vector3 targetLocal = Vector3.forward * wanderDistance;
            Vector3 targetWorld = gameObject.transform.TransformPoint(targetLocal);
            Vector3 targetLocalWander = wanderTarget + targetLocal;
            Vector3 targetWorldWander = gameObject.transform.TransformPoint(targetLocalWander);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetWorld);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(targetWorld, wanderRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(targetWorld, targetWorldWander);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, targetWorldWander);
        }
    }

    private void SetRandomDestination()
    {
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            Wander();
            Invoke(nameof(SetRandomDestination), Random.Range(minWaitTime, maxWaitTime));
        }
        else 
        {
            Invoke(nameof(SetRandomDestination), 1f);
        }
    }

    private void Seek(Vector3 target)
    {
        navAgent.SetDestination(target);
    }

    private void Wander()
    {
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + (Vector3.forward * wanderDistance);
        Vector3 targetWorld = gameObject.transform.TransformPoint(targetLocal);

        Seek(targetWorld);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void RandomResetWanderingTime() 
    {
        timePassed = 0;
        wanderingTime = Random.Range(5, 10);
        wanderingResetInvoked = navAgent.isStopped = false;
    }

    
}
