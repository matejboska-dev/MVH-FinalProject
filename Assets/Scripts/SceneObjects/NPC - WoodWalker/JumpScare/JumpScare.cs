using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public GameObject JumpScareImg;
    public AudioSource audioSource;

  
    void Start()
    {
        JumpScareImg.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !Input.GetKey(KeyCode.LeftControl))
        {
            JumpScareImg.SetActive(true);
            audioSource.Play();
            StartCoroutine(DisableImg());
        }
    }

    IEnumerator DisableImg()
    {
        yield return new WaitForSeconds(2);
        JumpScareImg.SetActive(false);
    }
}
