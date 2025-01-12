using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChoppableTree : MonoBehaviour
{
    public bool playerInRange;
    public bool canBeChopped;

    public float treeMaxHealth;
    public float treeHealth;

    public Animator animator;

    public float caloriesSpentChoppingWood = 20;

    public AudioSource dropItemSound;

    public AudioSource choppingWoodSound;



    void Start()
    {
        treeHealth = treeMaxHealth;    
        animator = transform.parent.transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        if (canBeChopped)
        {
            GlobalState.Instance.resourceHealth = treeHealth;
            GlobalState.Instance.resourceMaxHealth = treeMaxHealth;
        }
    }


    public void GetHit(){
        
        animator.SetTrigger("shake");
        treeHealth -=1;
        if (!choppingWoodSound.isPlaying)
            {
                choppingWoodSound.Play();
            }

        PlayerState.Instance.currentCalories -= caloriesSpentChoppingWood;

        if (treeHealth <= 0)
        {
            TreeIsDead();
        }
    }
    public void GetHitHarder(){
        
        animator.SetTrigger("shake");
        treeHealth -=2;
                if (!choppingWoodSound.isPlaying)
            {
                choppingWoodSound.Play();
            }

        PlayerState.Instance.currentCalories -= caloriesSpentChoppingWood;

        if (treeHealth <= 0)
        {
            TreeIsDead();
        }
    }
    public void GetHitHardest(){
        
        animator.SetTrigger("shake");
        treeHealth -=3;
        if (!choppingWoodSound.isPlaying)
        {
                choppingWoodSound.Play();
        }

        PlayerState.Instance.currentCalories -= caloriesSpentChoppingWood;

        if (treeHealth <= 0)
        {
            TreeIsDead();
        }
    }


    void TreeIsDead(){
        if (!dropItemSound.isPlaying)
        {
                dropItemSound.Play();
        }
        Vector3 treePosition = transform.position;
        Destroy(transform.parent.transform.parent.gameObject);
        canBeChopped = false;
        SelectionManager.Instance.selectedTree = null;
        SelectionManager.Instance.chopHolder.gameObject.SetActive(false);
        GameObject brokenTree = Instantiate(Resources.Load<GameObject>("ChoppedTree"),
            new Vector3(treePosition.x,treePosition.y,treePosition.z), Quaternion.Euler(0,0,0));
            
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

}
