using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public GameObject JumpScareImg;
    public AudioSource audioSource;

    private float cooldownTime = 60f; // Cooldown in seconds (1 minute)
    private bool isOnCooldown = false;

    void Start()
    {
        JumpScareImg.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !Input.GetKey(KeyCode.LeftControl) && !isOnCooldown)
        {
            JumpScareImg.SetActive(true);
            audioSource.Play();
            StartCoroutine(DisableImg());
            StartCoroutine(StartCooldown());
        }
    }

    IEnumerator DisableImg()
    {
        yield return new WaitForSeconds(1);
        JumpScareImg.SetActive(false);
    }

    IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }
}
