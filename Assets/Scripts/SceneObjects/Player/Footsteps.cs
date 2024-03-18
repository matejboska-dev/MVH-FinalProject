using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footstepsSound, sprintSound, crouchingSound;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                footstepsSound.enabled = false;
                sprintSound.enabled = true;
                crouchingSound.enabled = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                crouchingSound.enabled = true;
                footstepsSound.enabled = false;
                sprintSound.enabled = false;
            }
            else
            {
                footstepsSound.enabled = true;
                sprintSound.enabled = false;
                crouchingSound.enabled = false;
            }
        }
        else
        {
            footstepsSound.enabled = false;
            sprintSound.enabled = false;
            crouchingSound.enabled = false;
        }
    }
}