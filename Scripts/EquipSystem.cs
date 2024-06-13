using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }
 
    // -- UI -- //
    public GameObject quickSlotsPanel;

 
    public List<GameObject> quickSlotsList = new List<GameObject>();

    public GameObject  numbersHolder;
    public int selectedNumber = -1;
    public GameObject selectedItem;

    public string ItemModelName;

    public GameObject toolHolder;

    public GameObject selectedItemModel;

 
   
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
 
 
    private void Start()
    {
        PopulateSlotList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectQuickSlot(1);   
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);   
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);   
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);   
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);   
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectQuickSlot(6);   
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectQuickSlot(7);   
        }
    }
    
    void SelectQuickSlot(int number){

        if (CheckIfSlotIsFull(number) == true)
        {
            if (selectedNumber != number)
            {
                selectedNumber = number;

            //Unselect previously
                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                }
            selectedItem = getSelectedItem(number);
            selectedItem.GetComponent<InventoryItem>().isSelected = true;

            //Spawn Item Model
            SetEquippedModel(selectedItem);

            //Changing Color
            foreach (Transform child in numbersHolder.transform)
            {
                child.transform.Find("Text").GetComponent<Text>().color = Color.gray;
            }
            Text toBeChanged = numbersHolder.transform.Find("number" + number).transform.Find("Text").GetComponent<Text>();
            toBeChanged.color=  Color.white;
            }


            else
            {
                selectedNumber = -1; //null

                //Unselect preciously selected item
                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                    selectedItem = null;
                }

                //Destroy what we hold
                if (selectedItemModel != null)
                {
                    DestroyImmediate(selectedItemModel.gameObject);
                    selectedItemModel = null;
                }

                foreach (Transform child in numbersHolder.transform)
                {
                    child.transform.Find("Text").GetComponent<Text>().color = Color.gray;
                }

            }
        }

    }

    private void SetEquippedModel(GameObject selectedItem){

        //Destroy what we hold
        if (selectedItemModel != null)
        {
            DestroyImmediate(selectedItemModel.gameObject);
            selectedItemModel = null;
        }

        string selectedItemName = selectedItem.name.Replace("(Clone)","");

        selectedItemModel = Instantiate(Resources.Load<GameObject>(selectedItemName + "_Model"), 
            new Vector3(0.32f, 0.28f ,0.46f),Quaternion.Euler(0,70.98f,0));
            
        selectedItemModel.transform.SetParent(toolHolder.transform, false);

    }



    bool CheckIfSlotIsFull(int slotNumber){
        if (quickSlotsList[slotNumber-1].transform.childCount >0)
        {
            return true;
            
        }
        else
        {
            return false;
        }

    }
    GameObject getSelectedItem(int slotNumber){
        return quickSlotsList[slotNumber-1].transform.GetChild(0).gameObject;
    }


    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }
 
    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);
 
        InventorySystem.Instance.recalculateList();
 
    }
 
 
    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }
 
    public bool CheckIfFull()
    {
 
        int counter = 0;
 
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }
 
        if (counter == 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
