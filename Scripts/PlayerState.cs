using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance {
        get;
        set;
    }

    //Player Health
    public float currentHealth;
    public float maxHealth;




    //Player Calories
    public float currentCalories;
    public float maxCalories;


    //Player Hydration
    public float currentHydrationPercent;
    public float maxHydrationPercent;

    public bool isHydrationActive;

    public bool isDead;


    // Start is called before the first frame update
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
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;

        StartCoroutine(decreaseHydration());
        StartCoroutine(decreaseCalories());
        
    }
    IEnumerator  decreaseHydration(){
        while (true)
        {
            currentHydrationPercent -= 1;
            currentCalories -=1;
        
            yield return new WaitForSeconds(10f);
        }
    }
        IEnumerator  decreaseCalories(){
        while (true)
        {
            currentCalories -=1;     
            yield return new WaitForSeconds(5f);
        }
    }
    public void setHydration (float newHydration) {
        currentHydrationPercent = newHydration;
     }
    public void setHealth(float newHealth) { 
        currentHealth = newHealth;
    }
    public void setCalories (float newCalories) {
        currentCalories = newCalories;
     }

    // Update is called once per frame
    void Update()
    {

        //Testing Health bar
        if(Input.GetKeyDown(KeyCode.N)){
            currentHealth -=10;

        }
        if(currentHealth <= 0){
            isDead = true;
            

        }
        if(currentHealth > 0){
            isDead = false;

        }

        if(currentCalories <= 0){
            currentHealth -=1;
        }
        if(currentHydrationPercent <= 0f){
            currentHealth -=1;
        }
        
    }

}
