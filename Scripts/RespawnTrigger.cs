using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnTrigger : MonoBehaviour
{

    private void  OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             SceneManager.LoadScene("SampleScene");
        }
        
    }
}
