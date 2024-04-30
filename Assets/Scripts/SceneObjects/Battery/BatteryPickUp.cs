using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickUp : MonoBehaviour
{
    private bool inReach;

    public GameObject pickUpText;
    private GameObject flashlight;

    public AudioSource pickUpSound;

    void Start()
    {
        inReach = false;
        pickUpText.SetActive(false);
        flashlight = GameObject.Find("flashlight");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inReach = true;
            pickUpText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inReach = false;
            pickUpText.SetActive(false);
        }
    }

    void Update()
    {
        Debug.Log("Niggers");
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            if (flashlight != null)
            {
                flashlight.GetComponent<FlashlightAdvanced>().batteries += 1;
                pickUpSound.Play();
                inReach = false;
                pickUpText.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Flashlight GameObject or FlashlightAdvanced component is missing!");
            }
        }
    }
}
