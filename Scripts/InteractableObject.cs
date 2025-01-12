using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{   
    public bool playerInRange;
    public string ItemName;
    public string GetItemName(){
        return ItemName;
    }

    private void  OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            playerInRange = true;
        }
    }

    private  void OnTriggerExit(Collider other)
    {
        playerInRange = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && SelectionManager.Instance.OnTarget &&SelectionManager.Instance.selectedObject == gameObject)
        {
            if(!InventorySystem.Instance.CheckFull()){
            InventorySystem.Instance.addToInventory(ItemName);
            Destroy(gameObject);
            }

            else{
                Debug.Log("Inventory is full");
            }

        }

    }
}
