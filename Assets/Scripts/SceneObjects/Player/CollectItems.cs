using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
    public float collectRange = 3f; // Adjust the range within which the player can collect the item
    public string collectText = "Press E to collect";

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange)
        {
            // Display collect text on the screen
            DisplayCollectText();

            // Check for player input to collect the item
            if (Input.GetKeyDown(KeyCode.E))
            {
                CollectItem();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void DisplayCollectText()
    {
        // You can implement your own UI system for displaying text on the screen
        // For simplicity, we'll use Debug.Log for demonstration purposes
        Debug.Log(collectText);
    }

    void CollectItem()
    {
        // Implement the logic for collecting the item here
        // For example, you can increase the player's score, health, or any other parameter
        // Destroy the collectable item after collecting, if needed
        Destroy(gameObject);
    }
}