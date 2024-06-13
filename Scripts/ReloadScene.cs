using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{


    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    } 
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        if (scene.name == "SampleScene")
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = new Vector3(28.432f, 11.81f, -14.13f);
            gameObject.GetComponent<CharacterController>().enabled = true;           
        }
        if (scene.name == "Level1")
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = new Vector3(7.993f, 10.864f, -10.872f);
            gameObject.GetComponent<CharacterController>().enabled = true;           
        }
        
        if (scene.name == "Level2")
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = new Vector3(21.6942f, 7.689f, -11.80677f);
            gameObject.GetComponent<CharacterController>().enabled = true;        
        }
        
        if (scene.name == "Level3")
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = new Vector3(17.52051f, 2.162f, -23.79795f);
            gameObject.GetComponent<CharacterController>().enabled = true;           
        }
    }
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
    }
    
    void  Update()
    {
    
        if (PlayerState.Instance.currentHealth <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
        
    }
    
}
