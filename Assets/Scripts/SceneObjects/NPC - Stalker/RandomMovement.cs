using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; // Radius of sphere
    public float chaseRadius; // Radius within which Stalker will chase the player
    public Transform player; // Reference to the player's transform

    public Transform centrePoint; // Centre of the area the agent wants to move around in
    // Instead of centrePoint, you can set it as the transform of the agent if you don't care about a specific area

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Check if player is within chase radius
        if (Vector3.Distance(transform.position, player.position) <= chaseRadius)
        {
            // Chase the player
            agent.SetDestination(player.position);
        }
        else
        {
            // Random movement within the defined area
            if (agent.remainingDistance <= agent.stoppingDistance) // Done with path
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point)) // Pass in our centre point and radius of area
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); // So you can see with gizmos
                    agent.SetDestination(point);
                }
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; // Random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            // The 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
