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
    public Light flashlight; // Reference to the light GameObject

    public Transform centrePoint; // Centre of the area the agent wants to move around in

    private bool isChasing = false;

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
            isChasing = true;
        }
        else
        {
            isChasing = false;
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

        // Check if the NPC is chasing the player
        if (isChasing)
        {
            // Check if the flashlight is on and pointing at the NPC
            if (flashlight.enabled)
            {
                Vector3 directionToNPC = transform.position - flashlight.transform.position;
                float angle = Vector3.Angle(flashlight.transform.forward, directionToNPC);

                if (angle < flashlight.spotAngle / 2)
                {
                    // Make the NPC move away from the player
                    Vector3 awayFromPlayerDirection = transform.position - player.position;
                    Vector3 destination = transform.position + awayFromPlayerDirection.normalized * range;
                    agent.SetDestination(destination);
                    return; // Stop NPC behavior if it's moving away
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
