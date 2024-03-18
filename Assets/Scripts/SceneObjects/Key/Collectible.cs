using System;
using System.Collections;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public static event Action OnCollected;
    public static int total;

    private void Awake()
    {
        total++;
    }

    private void Update()
    {
        // Check if the player presses 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check if the player is within the collider of the collectible
            Collider playerCollider = GetPlayerCollider();
            if (playerCollider != null && GetComponent<Collider>().bounds.Intersects(playerCollider.bounds))
            {
                // Collect the item
                CollectItem();
            }
        }
    }

    private void CollectItem()
    {
        // Trigger collection event
        OnCollected?.Invoke();
        // Destroy the collectible object
        Destroy(gameObject);
    }

    private Collider GetPlayerCollider()
    {
        // Find the player GameObject with the "Player" tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Get the Collider component of the player GameObject
            return player.GetComponent<Collider>();
        }
        return null;
    }

    
}
