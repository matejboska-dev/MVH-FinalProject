using UnityEngine;
using UnityEngine.AI;

public class WoodWalkerBehaviour : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject normalModel; // Reference to the normal model GameObject
    public GameObject chasingModel; // Reference to the chasing model GameObject
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    private bool isChasing = false; // Flag to track whether NPC is chasing or not

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); 
       
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
        if (other.CompareTag("Player") )
        {
            StopChasing();
        }
    }

    void Update()
    {
        if (isChasing)
        {
            agent.SetDestination(player.position);
        }
        else if (!Input.GetKey(KeyCode.LeftControl)) {
            // If not already chasing, check if the player is within range and not holding down left control
             
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                if (distanceToPlayer <= agent.stoppingDistance)
                {
                    StartChasing();
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
        SetChasingModelActive(false);
        SetNormalModelActive(true);

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
