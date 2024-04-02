using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareScript : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public AudioClip jumpScareSound; // Jump scare sound effect
    public Animator npcAnimator; // Reference to the NPC's Animator component
    public float teleportDistance = 3f; // Distance to teleport in front of the player
    public float teleportHeight = 1f; // Height at which to teleport

    private AudioSource audioSource; // Reference to the AudioSource component
    private bool hasJumpScared = false; // Flag to track if jump scare has been triggered

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !hasJumpScared)
        {
            // Play jump scare sound
            if (jumpScareSound != null)
            {
                audioSource.PlayOneShot(jumpScareSound);
            }

            // Teleport NPC in front of the player
            Vector3 teleportPosition = player.transform.position + player.transform.forward * teleportDistance;
            teleportPosition.y = teleportHeight; // Adjust height
            transform.position = teleportPosition;

            // Play jump scare animation
            npcAnimator.SetTrigger("JumpScare");

            // Set flag to prevent repeated jump scares
            hasJumpScared = true;
        }
    }
}
