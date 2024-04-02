using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WoodWalkerBehaviour : MonoBehaviour
{
    public float detectionRadius = 10f; // Radius within which the player can trigger the NPC
    public float chaseSpeed = 5f; // Speed at which the NPC chases the player
    public GameObject awakeModel; // Reference to the awake model GameObject
    public GameObject asleepModel; // Reference to the asleep model GameObject

    private Transform player; // Reference to the player's transform
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    private Animator animator; // Reference to the Animator component
    private bool isPlayerNearby = false; // Flag to track if player is nearby

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player GameObject
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        animator = GetComponent<Animator>(); // Get the Animator component
        SetNPCModel(asleepModel); // Set initial model to asleep model
    }

    private void Update()
    {
        // Check if player is within detection radius and not crouching
        if (isPlayerNearby && !Input.GetKey(KeyCode.LeftControl))
        {
            // Trigger jump scare animation
            animator.SetTrigger("JumpScare");

            // Start chasing the player
            ChasePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            SetNPCModel(awakeModel); // Change model to awake when player is nearby
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // If player exits NPC's trigger radius, stop chasing
            isPlayerNearby = false;
            agent.ResetPath();
            SetNPCModel(asleepModel); // Change model back to asleep
        }
    }

    private void ChasePlayer()
    {
        // Set destination to player's position
        agent.SetDestination(player.position);
        agent.speed = chaseSpeed; // Set chase speed
    }

    private void SetNPCModel(GameObject model)
    {
        // Disable all models
        awakeModel.SetActive(false);
        asleepModel.SetActive(false);

        // Enable the specified model
        model.SetActive(true);
    }
}
