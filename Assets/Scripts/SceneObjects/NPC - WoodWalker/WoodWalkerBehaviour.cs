using UnityEngine;
using UnityEngine.AI;

public class WoodWalkerBehaviour : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject normalModel; // Reference to the normal model GameObject
    public GameObject chasingModel; // Reference to the chasing model GameObject
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    private bool isChasing = false; // Flag to track whether NPC is chasing or not
    private Vector3 startingPosition; // Store the NPC's starting position

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startingPosition = transform.position; // Save the starting position

        SetNormalModelActive(true);
        SetChasingModelActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Input.GetKey(KeyCode.LeftControl))
        {
            StartChasing();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopChasing();
        }
    }

    void Update()
    {
        if (isChasing)
        {
            agent.SetDestination(player.position);
            SetChasingModelActive(true); // Keep chasing model active while chasing
        }
        else if (!Input.GetKey(KeyCode.LeftControl))
        {
            // Check if the player is within range
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= agent.stoppingDistance)
            {
                StartChasing();
            }
            else
            {
                // Check distance to starting position
                float distanceToStart = Vector3.Distance(transform.position, startingPosition);

                if (distanceToStart > agent.stoppingDistance)
                {
                    // Set destination to starting position
                    agent.SetDestination(startingPosition);
                    SetChasingModelActive(true);  // Keep chasing model active while returning
                }
                else
                {
                    // Reached starting position, switch to normal model
                    SetChasingModelActive(false);
                    SetNormalModelActive(true);
                }

                // Check if agent is not moving and switch to normal model
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    SetChasingModelActive(false);
                    SetNormalModelActive(true);
                }
            }
        }
    }

    void StartChasing()
    {
        isChasing = true;
        SetNormalModelActive(false);
        SetChasingModelActive(true);

        Debug.Log("NPC is now chasing the player!");
    }

    void StopChasing()
    {
        isChasing = false;

        Debug.Log("NPC has stopped chasing the player!");
    }

    void SetNormalModelActive(bool active)
    {
        normalModel.SetActive(active);
    }

    void SetChasingModelActive(bool active)
    {
        chasingModel.SetActive(active);
    }
}
