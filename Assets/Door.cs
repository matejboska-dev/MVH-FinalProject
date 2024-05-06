using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject[] collectibles;

    private bool isOpen = false;

    void OnTriggerEnter(Collider other)
    {
        if (!isOpen && other.CompareTag("Player"))
        {
            if (CheckCollectibles())
            {
                OpenDoor();
            }
        }
    }

    bool CheckCollectibles()
    {
        foreach (GameObject collectible in collectibles)
        {
            if (collectible != null)
            {
                return false; // At least one collectible is still present
            }
        }
        return true; // All collectibles have been collected
    }

    void OpenDoor()
    {
        // Your code to open the door goes here
        Debug.Log("Door is open!");
        isOpen = true;
    }
}
