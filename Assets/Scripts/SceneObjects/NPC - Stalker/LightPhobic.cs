using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPhobic : MonoBehaviour
{
    public float fearRadius = 10f; // Radius within which the NPC fears light
    public Transform flashlight; // Reference to the flashlight transform

    private Transform player; // Reference to the player's transform

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has a "Player" tag
    }

    void Update()
    {
        if (flashlight != null)
        {
            // Check if the flashlight is within the fear radius
            float distanceToFlashlight = Vector3.Distance(transform.position, flashlight.position);
            if (distanceToFlashlight <= fearRadius)
            {
                // There's light, move away from it
                Vector3 direction = transform.position - flashlight.position;
                MoveAwayFromLight(direction);
            }
        }
    }

    void MoveAwayFromLight(Vector3 lightDirection)
    {
        // Calculate the destination away from the light
        Vector3 destination = transform.position + lightDirection.normalized * fearRadius;
        // Move towards the destination
        transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime);
    }
}
