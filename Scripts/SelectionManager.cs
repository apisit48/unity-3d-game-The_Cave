using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance{
        get;
        set;
    }

    public GameObject Interaction_Info_UI;
    public bool OnTarget;
    public GameObject selectedObject;
    Text interaction_text;

    public GameObject selectedTree;
    public GameObject selectedRock;

    public GameObject selectedEnemy;
    public GameObject chopHolder;

 
    private void Start()
    {
        OnTarget = false;
        interaction_text = Interaction_Info_UI.GetComponent<Text>();
    }
 
     void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            
            var selectionTransform = hit.transform;

            InteractableObject interactableObj = selectionTransform.GetComponent<InteractableObject>();


            ChoppableTree choppableTree = selectionTransform.GetComponent<ChoppableTree>();
            Mineable mineable = selectionTransform.GetComponent<Mineable>();
            Enemy enemy = selectionTransform.GetComponent<Enemy>();
            
            
            if (choppableTree && choppableTree.playerInRange)
            {
                choppableTree.canBeChopped = true;
                selectedTree = choppableTree.gameObject;
                chopHolder.gameObject.SetActive(true);
                
            }
            else {
                if (selectedTree != null)
                {
                    selectedTree.gameObject.GetComponent<ChoppableTree>().canBeChopped = false;
                    selectedTree = null;
                    chopHolder.gameObject.SetActive(false);
                }
            }
            
            
            if (mineable && mineable.playerInRange)
            {
                mineable.canBeMined = true;
                selectedRock = mineable.gameObject;
                chopHolder.gameObject.SetActive(true);
                
            }
            else {
                if (selectedRock != null)
                {
                    selectedRock.gameObject.GetComponent<Mineable>().canBeMined = false;
                    selectedRock = null;
                    chopHolder.gameObject.SetActive(false);
                }
            }


 
            if (interactableObj && interactableObj.playerInRange)
            {

                OnTarget  = true;
                selectedObject = interactableObj.gameObject;
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                Interaction_Info_UI.SetActive(true);

            }
            else 
            { 
                OnTarget  = false;
                Interaction_Info_UI.SetActive(false);
            }
 
        }

        else 
        { 
            OnTarget  = false;
                Interaction_Info_UI.SetActive(false);
        }

    }

    public void DisableSelection(){
        Interaction_Info_UI.SetActive(false);
        selectedObject = null;
    }
    
    public void EnableSelection(){
        Interaction_Info_UI.SetActive(true);
    }
}
