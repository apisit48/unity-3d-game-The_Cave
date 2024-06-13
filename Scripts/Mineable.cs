using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Mineable : MonoBehaviour
{
    public bool playerInRange;
    public bool canBeMined;

    public float rockMaxHealth;
    public float rockHealth;

    public Animator animator;

    public float caloriesSpentMining = 20;

    public AudioSource dropItemSound;



    void Start()
    {
        rockHealth = rockMaxHealth;    
        animator = transform.parent.transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        if (canBeMined)
        {
            GlobalState.Instance.resourceHealth = rockHealth;
            GlobalState.Instance.resourceMaxHealth = rockMaxHealth;
        }
    }


    public void GetHit(){
        
        animator.SetTrigger("shake");
        rockHealth -=1;

        PlayerState.Instance.currentCalories -= caloriesSpentMining;

        if (rockHealth <= 0)
        {
            RockIsDead();
        }
    }

    public void GetHitHarder(){
        
        animator.SetTrigger("shake");
        rockHealth -= 4;

        PlayerState.Instance.currentCalories -= caloriesSpentMining;

        if (rockHealth <= 0)
        {
            RockIsDead();
        }
    }

    public void GetHitHardest(){
        
        animator.SetTrigger("shake");
        rockHealth -=10;

        PlayerState.Instance.currentCalories -= caloriesSpentMining;

        if (rockHealth <= 0)
        {
            RockIsDead();
        }
    }


    void RockIsDead(){

        if (gameObject.name == "IronOre_base")
        {
            Vector3 rockPosition = transform.position;
            Destroy(transform.parent.transform.parent.gameObject);
            canBeMined = false;
            SelectionManager.Instance.selectedRock = null;
            SelectionManager.Instance.chopHolder.gameObject.SetActive(false);
            GameObject brokenRock = Instantiate(Resources.Load<GameObject>("MinedRock"),
            new Vector3(rockPosition.x,rockPosition.y,rockPosition.z), Quaternion.Euler(0,0,0));
            dropItemSound.Play();
        }

        if (gameObject.name == "CrystalOre_base")
        {
            Vector3 rockPosition = transform.position;
            Destroy(transform.parent.transform.parent.gameObject);
            canBeMined = false;
            SelectionManager.Instance.selectedRock = null;
            SelectionManager.Instance.chopHolder.gameObject.SetActive(false);
            GameObject brokenRock = Instantiate(Resources.Load<GameObject>("MinedCrystal"),
            new Vector3(rockPosition.x,rockPosition.y,rockPosition.z), Quaternion.Euler(0,0,0));
            dropItemSound.Play();
        }

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
