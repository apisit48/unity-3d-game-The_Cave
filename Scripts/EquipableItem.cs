using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableItem : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    public string ItemName;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //left mouse button click
        if (Input.GetMouseButtonDown(0) && InventorySystem.Instance.isOpen == false && CraftingSystem.Instance.isOpen == false)
        {

            animator.SetTrigger("hit");
        }
        
    }

    public void GetHit(){
        GameObject selectedTree = SelectionManager.Instance.selectedTree;
        if (selectedTree != null && ItemName == "Axe_Model")
        {
                selectedTree.GetComponent<ChoppableTree>().GetHit();
        }
        if (selectedTree != null && ItemName == "IronAxe_Model")
        {
                selectedTree.GetComponent<ChoppableTree>().GetHitHarder();
        }
        if (selectedTree != null && ItemName == "CrystalAxe_Model")
        {
                selectedTree.GetComponent<ChoppableTree>().GetHitHardest();
        }

        GameObject selectedRock = SelectionManager.Instance.selectedRock;
        if (selectedRock != null&& ItemName == "PickAxe_Model")
        {
                selectedRock.GetComponent<Mineable>().GetHit();
        }
        
        if (selectedRock != null&& ItemName == "IronPickAxe_Model")
        {
                selectedRock.GetComponent<Mineable>().GetHitHarder();
        }
        if (selectedRock != null&& ItemName == "CrystalPickAxe_Model")
        {
                selectedRock.GetComponent<Mineable>().GetHitHardest();
        }
    }
}
