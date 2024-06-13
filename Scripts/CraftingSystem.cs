using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingSystem : MonoBehaviour
{   
    public GameObject craftingScreenUI;
    public GameObject toolScreenUI;
    public List<string> inventoryItemList = new List<string>();
   


    //CategoryButton

    Button toolsBTN;


    //Craft Button

    Button craftAxeBTN;
    Button craftPickAxeBTN;
    Button craftIronHiltBTN;
    Button craftCrystalHiltBTN;
    Button craftHandleBTN;
    Button craftIronAxeBTN;
    Button craftCrystalAxeBTN;
    Button craftIronPickAxeBTN;
    Button craftCrystalPickAxeBTN;
    

    //Requirment Text
    Text AxeReq1,AxeReq2;
    Text PickAxeReq1,PickAxeReq2;
    Text IronHiltReq1;
    Text CrystalHiltReq1;
    Text HandleReq1;
    Text IronAxeReq1,IronAxeReq2,IronAxeReq3;
    Text IronPickAxeReq1,IronPickAxeReq2,IronPickAxeReq3;
    Text CrystalAxeReq1,CrystalAxeReq2,CrystalAxeReq3;
    Text CrystalPickAxeReq1,CrystalPickAxeReq2,CrystalPickAxeReq3;

    public bool isOpen;

    public AudioSource craftItemSound;



    public static CraftingSystem Instance {
        get;
        set;
    }

    void Awake()
    {

        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }

        
    }

    void Start()
    {
        Blueprint AxeBLP = new Blueprint("Axe",2,"Stone",3,"Stick",3);
        Blueprint PickAxeBLP = new Blueprint("PickAxe",2,"Stone",4,"Stick",2);
        Blueprint IronHiltBLP = new Blueprint("IronHilt",1,"Iron",2);
        Blueprint CrystalHiltBLP = new Blueprint("CrystalHilt",1,"Crystal",2);
        Blueprint HandleBLP = new Blueprint("Handle",1,"Log",3);
        Blueprint IronAxeBLP = new Blueprint("IronAxe",3,"Iron",3,"Handle",1,"IronHilt",1);
        Blueprint IronPickAxeBLP = new Blueprint("IronPickAxe",3,"Iron",3,"Handle",1,"IronHilt",1);
        Blueprint CrystalAxeBLP = new Blueprint("CrystalAxe",3,"Crystal",3,"Handle",1,"CrystalHilt",1);
        Blueprint CrystalPickAxeBLP = new Blueprint("CrystalPickAxe",3,"Crystal",3,"Handle",1,"CrystalHilt",1);
        
        isOpen = false;
        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate{OpenToolCategory();});


        //Axe
        AxeReq1 = toolScreenUI.transform.Find("Axe").transform.Find("Req").GetComponent<Text>();
        AxeReq2 = toolScreenUI.transform.Find("Axe").transform.Find("Req2").GetComponent<Text>();
        craftAxeBTN = toolScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        print(craftAxeBTN.ToString());
        craftAxeBTN.onClick.AddListener(delegate{CraftAnyItem(AxeBLP);});



        // PickAxe
        PickAxeReq1 = toolScreenUI.transform.Find("PickAxe").transform.Find("Req").GetComponent<Text>();
        PickAxeReq2 = toolScreenUI.transform.Find("PickAxe").transform.Find("Req2").GetComponent<Text>();
        craftPickAxeBTN = toolScreenUI.transform.Find("PickAxe").transform.Find("Button").GetComponent<Button>();
        print(craftPickAxeBTN.ToString());
        craftPickAxeBTN.onClick.AddListener(delegate{CraftAnyItem(PickAxeBLP);});

        //IronHilt
        IronHiltReq1 = toolScreenUI.transform.Find("IronHilt").transform.Find("Req").GetComponent<Text>();
        craftIronHiltBTN = toolScreenUI.transform.Find("IronHilt").transform.Find("Button").GetComponent<Button>();
        print(craftIronHiltBTN.ToString());
        craftIronHiltBTN.onClick.AddListener(delegate{CraftAnyItem(IronHiltBLP);});
        
        //CrystalHilt
        CrystalHiltReq1 = toolScreenUI.transform.Find("CrystalHilt").transform.Find("Req").GetComponent<Text>();
        craftCrystalHiltBTN = toolScreenUI.transform.Find("CrystalHilt").transform.Find("Button").GetComponent<Button>();
        print(craftCrystalHiltBTN.ToString());
        craftCrystalHiltBTN.onClick.AddListener(delegate{CraftAnyItem(CrystalHiltBLP);});

        //Handle
        HandleReq1 = toolScreenUI.transform.Find("Handle").transform.Find("Req").GetComponent<Text>();
        craftHandleBTN = toolScreenUI.transform.Find("Handle").transform.Find("Button").GetComponent<Button>();
        print(craftHandleBTN.ToString());
        craftHandleBTN.onClick.AddListener(delegate{CraftAnyItem(HandleBLP);});

        //IronAxe
        IronAxeReq1 = toolScreenUI.transform.Find("IronAxe").transform.Find("Req").GetComponent<Text>();
        IronAxeReq2 = toolScreenUI.transform.Find("IronAxe").transform.Find("Req2").GetComponent<Text>();
        IronAxeReq3 = toolScreenUI.transform.Find("IronAxe").transform.Find("Req3").GetComponent<Text>();
        craftIronAxeBTN = toolScreenUI.transform.Find("IronAxe").transform.Find("Button").GetComponent<Button>();
        print(craftIronAxeBTN.ToString());
        craftIronAxeBTN.onClick.AddListener(delegate{CraftAnyItem(IronAxeBLP);});
        
        //IronPickAxe
        IronPickAxeReq1 = toolScreenUI.transform.Find("IronPickAxe").transform.Find("Req").GetComponent<Text>();
        IronPickAxeReq2 = toolScreenUI.transform.Find("IronPickAxe").transform.Find("Req2").GetComponent<Text>();
        IronPickAxeReq3 = toolScreenUI.transform.Find("IronPickAxe").transform.Find("Req3").GetComponent<Text>();
        craftIronPickAxeBTN = toolScreenUI.transform.Find("IronPickAxe").transform.Find("Button").GetComponent<Button>();
        print(craftIronPickAxeBTN.ToString());
        craftIronPickAxeBTN.onClick.AddListener(delegate{CraftAnyItem(IronPickAxeBLP);});

        //CrystalAxe
        CrystalAxeReq1 = toolScreenUI.transform.Find("CrystalAxe").transform.Find("Req").GetComponent<Text>();
        CrystalAxeReq2 = toolScreenUI.transform.Find("CrystalAxe").transform.Find("Req2").GetComponent<Text>();
        CrystalAxeReq3 = toolScreenUI.transform.Find("CrystalAxe").transform.Find("Req3").GetComponent<Text>();
        craftCrystalAxeBTN = toolScreenUI.transform.Find("CrystalAxe").transform.Find("Button").GetComponent<Button>();
        print(craftCrystalAxeBTN.ToString());
        craftCrystalAxeBTN.onClick.AddListener(delegate{CraftAnyItem(CrystalAxeBLP);});

        //CrystalPickAxe
        CrystalPickAxeReq1 = toolScreenUI.transform.Find("CrystalPickAxe").transform.Find("Req").GetComponent<Text>();
        CrystalPickAxeReq2 = toolScreenUI.transform.Find("CrystalPickAxe").transform.Find("Req2").GetComponent<Text>();
        CrystalPickAxeReq3 = toolScreenUI.transform.Find("CrystalPickAxe").transform.Find("Req3").GetComponent<Text>();
        craftCrystalPickAxeBTN = toolScreenUI.transform.Find("CrystalPickAxe").transform.Find("Button").GetComponent<Button>();
        print(craftCrystalPickAxeBTN.ToString());
        craftCrystalPickAxeBTN.onClick.AddListener(delegate{CraftAnyItem(CrystalPickAxeBLP);});

    }

    void CraftAnyItem(Blueprint blueprintToCraft){
        
        InventorySystem.Instance.addToInventory(blueprintToCraft.ItemName);
        if (blueprintToCraft.numOfReq == 1)
        {
            InventorySystem.Instance.removeItem(blueprintToCraft.Req1,blueprintToCraft.Req1Amount);
        }
        else if (blueprintToCraft.numOfReq == 2){
            InventorySystem.Instance.removeItem(blueprintToCraft.Req1,blueprintToCraft.Req1Amount);
            InventorySystem.Instance.removeItem(blueprintToCraft.Req2,blueprintToCraft.Req2Amount);
        }
        else if (blueprintToCraft.numOfReq == 3){
            InventorySystem.Instance.removeItem(blueprintToCraft.Req1,blueprintToCraft.Req1Amount);
            InventorySystem.Instance.removeItem(blueprintToCraft.Req2,blueprintToCraft.Req2Amount);
            InventorySystem.Instance.removeItem(blueprintToCraft.Req3,blueprintToCraft.Req3Amount);
        }

        craftItemSound.Play();
        StartCoroutine(calculate());
    }
    
    
    public IEnumerator  calculate(){
        yield return 0;
        InventorySystem.Instance.recalculateList();
        RefreshNeededItems();
    
    }


    public void RefreshNeededItems()
    {
        
     int stone_count = 0;
     int stick_count = 0;
     int iron_count = 0;
     int iron_hilt = 0;
     int crystal_count = 0;
     int crystal_hilt = 0;
     int log_count = 0;
     int handle_count = 0;

        inventoryItemList = InventorySystem.Instance.ItemList;

        foreach (string itemName in inventoryItemList)
        {
            switch(itemName){
                case "Stone":
                    stone_count +=1;
                    break;
                case "Stick":
                    stick_count +=1;
                    break;
                case "Iron":
                    iron_count +=1;
                    break;
                case "CrystalHilt":
                    crystal_hilt +=1;
                    break;
                case "IronHilt":
                    iron_hilt +=1;
                    break;
                case "Log":
                    log_count +=1;
                    break;
                case "Handle":
                    handle_count +=1;
                    break;
                case "Crystal":
                    crystal_count +=1;
                    break;
                
            }
        }

        //AXE

        AxeReq1.text = "3 Stones[" + stone_count +"]";
        AxeReq2.text = "3 Sticks[" + stick_count +"]";

        if(stone_count >=3 && stick_count >= 3) {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else{
            craftAxeBTN.gameObject.SetActive(false);
        }

        //PickAxe
        PickAxeReq1.text = "4 Stones[" + stone_count +"]";
        PickAxeReq2.text = "2 Sticks[" + stick_count +"]";

        if(stone_count >=4 && stick_count >= 2) {
            craftPickAxeBTN.gameObject.SetActive(true);
        }
        else{
            craftPickAxeBTN.gameObject.SetActive(false);
        }

        //IronHilt
        IronHiltReq1.text = "2 Iron[" + iron_count +"]";

        if(iron_count >=2) {
            craftIronHiltBTN.gameObject.SetActive(true);
        }
        else{
            craftIronHiltBTN.gameObject.SetActive(false);
        }

        //CrystalHilt
        CrystalHiltReq1.text = "2 Crystal[" + crystal_count +"]";

        if(crystal_count >=2) {
            craftCrystalHiltBTN.gameObject.SetActive(true);
        }
        else{
            craftCrystalHiltBTN.gameObject.SetActive(false);
        }

        //Handle
        HandleReq1.text = "3 Log[" + log_count +"]";

        if(log_count >=3) {
            craftHandleBTN.gameObject.SetActive(true);
        }
        else{
            craftHandleBTN.gameObject.SetActive(false);
        }

        //Iron Axe

        IronAxeReq1.text = "3 Iron[" + iron_count +"]";
        IronAxeReq2.text = "1 Handle[" + handle_count +"]";
        IronAxeReq3.text = "1 Iron Hilt[" + iron_hilt +"]";

        if(iron_count >=3 && handle_count >= 1 && iron_hilt >= 1) {
            craftIronAxeBTN.gameObject.SetActive(true);
        }
        else{
            craftIronAxeBTN.gameObject.SetActive(false);
        }

        //Iron PickAxe

        IronPickAxeReq1.text = "3 Iron[" + iron_count +"]";
        IronPickAxeReq2.text = "1 Handle[" + handle_count +"]";
        IronPickAxeReq3.text = "1 Iron Hilt[" + iron_hilt +"]";

        if(iron_count >=3 && handle_count >= 1 && iron_hilt >= 1) {
            craftIronPickAxeBTN.gameObject.SetActive(true);
        }
        else{
            craftIronPickAxeBTN.gameObject.SetActive(false);
        }

        //Crystal Axe

        CrystalAxeReq1.text = "3 Crystal[" + crystal_count +"]";
        CrystalAxeReq2.text = "1 Handle[" + handle_count +"]";
        CrystalAxeReq3.text = "1 Crystal Hilt[" + crystal_hilt +"]";

        if(crystal_count >=3 && handle_count >= 1 && crystal_hilt >= 1) {
            craftCrystalAxeBTN.gameObject.SetActive(true);
        }
        else{
            craftCrystalAxeBTN.gameObject.SetActive(false);
        }

        //Crystal PickAxe

        CrystalPickAxeReq1.text = "3 Crystal[" + crystal_count +"]";
        CrystalPickAxeReq2.text = "1 Handle[" + handle_count +"]";
        CrystalPickAxeReq3.text = "1 Crystal Hilt[" + crystal_hilt +"]";

        if(crystal_count >=3 && handle_count >= 1 && crystal_hilt >= 1) {
            craftCrystalPickAxeBTN.gameObject.SetActive(true);
        }
        else{
            craftCrystalPickAxeBTN.gameObject.SetActive(false);
        }

    }

    void OpenToolCategory(){
        craftingScreenUI.SetActive(false);
        toolScreenUI.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isOpen)
        {
        
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
            isOpen = true;
 
        }
        else if (Input.GetKeyDown(KeyCode.T) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SelectionManager.Instance.EnableSelection();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
            }
            isOpen = false;
        }
    }
}
