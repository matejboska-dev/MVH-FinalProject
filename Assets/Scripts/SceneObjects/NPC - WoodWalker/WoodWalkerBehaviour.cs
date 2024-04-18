using UnityEngine;
using UnityEngine.AI;

public class WoodWalkerBehaviour : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    private bool isChasing = false; // Flag to track whether NPC is chasing or not

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get reference to NavMeshAgent component
    }

    void Update()
    {
        if (!isChasing)
        {
            // If not already chasing, check if the player is within range
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= agent.stoppingDistance)
            {
                StartChasing();
            }
        }
        else
        {
            // If already chasing, set the destination to the player's position
            agent.SetDestination(player.position);

            // Check if the player is within stopping distance
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > agent.stoppingDistance)
            {
                StopChasing();
            }
        }
    }

    public void StartChasing()
    {
        isChasing = true;
        // You might want to add additional behavior here when NPC starts chasing
        Debug.Log("NPC is now chasing the player!");
    }

    public void StopChasing()
    {
        isChasing = false;
        // You might want to add additional behavior here when NPC stops chasing
        Debug.Log("NPC has stopped chasing the player!");
    }
}
