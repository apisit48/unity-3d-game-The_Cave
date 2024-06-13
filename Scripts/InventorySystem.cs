using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
 
   public static InventorySystem Instance { 
    get; 
    set; 
    }
    
    public GameObject ItemInfoUI;
    public GameObject inventoryScreenUI;
    
    
    public List<GameObject> slotList =  new List<GameObject>();

    public List<string> ItemList=  new List<string>();

    private GameObject ItemToAdd;
    private GameObject whatSlotToEquip;

    public bool isOpen;

    public bool isFull;

    public AudioSource dropItemSound;



 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
 
 
    void Start()
    {
        isOpen = false;
        isFull = false;
        PopulateSlotList();
        Cursor.visible = false;
    }
 
    
    private void PopulateSlotList(){
        foreach(Transform child in inventoryScreenUI.transform){
            if(child.CompareTag("Slot")){
                slotList.Add(child.gameObject);
            }
        }
    }



    void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
        
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;

            isOpen = true;
            
 
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            if (!CraftingSystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SelectionManager.Instance.EnableSelection();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
            }
            
            isOpen = false;
            
        }
    }

    

    public void addToInventory(string ItemName){

        whatSlotToEquip = FindNextEmptySlot();
        ItemToAdd = Instantiate(Resources.Load<GameObject>(ItemName), 
                        whatSlotToEquip.transform.position,whatSlotToEquip.transform.rotation);
            
        ItemToAdd.transform.SetParent(whatSlotToEquip.transform);
        ItemList.Add(ItemName);
        
        recalculateList();
        CraftingSystem.Instance.RefreshNeededItems();

    }

    public bool CheckFull(){
        int count = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                count += 1;
            }

        }
        if(count == 15){
            return true;
         }
         else{
            return false;
         }
    }

    private GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotList){
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
            
        }

        return new GameObject();
    }

    public void removeItem(string nameToRemove, int amountToRemove){
        int count = amountToRemove;

        for (var i =slotList.Count-1 ; i >= 0 ;i--)       {
            if (slotList[i].transform.childCount > 0 )
            {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && count != 0)
                {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    count -= 1;
                }
            }
        }
        
        dropItemSound.Play();
        
        recalculateList();
        CraftingSystem.Instance.RefreshNeededItems();

    }

    public void recalculateList(){
        ItemList.Clear();
        
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;
                string str2 = "(Clone)";
                string result = name.Replace(str2, "");
                ItemList.Add(result);
            }
            
        }
    }
}
