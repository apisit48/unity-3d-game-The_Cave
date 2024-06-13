using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public float speed = 12f;
    
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    public float staggerForce = 5f;
    public float bulletSpeed = 10f;

    [SerializeField ]private Transform respawnPoint;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    public GameObject bulletPrefab;

    public AudioSource WalkingSound;

    Vector3 velocity;
 
    bool isGrounded;
    bool isWalking;
    

   
    void Update()
    {   
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W))
        {
            if (!WalkingSound.isPlaying)
            {
                WalkingSound.Play();
            }
        
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W))
        {

             WalkingSound.Stop();
            
        
        }
        if (PlayerState.Instance.currentHealth <= 0)
        {
            Debug.Log("Player died from damage!");
            Dead();
        }
        movement();
        
        if (Input.GetKeyDown(KeyCode.Q)) {
            Shoot();
        }


        
    }

    void movement(){

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            
        }

        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
 
        Vector3 move = transform.right * x + transform.forward * z;
 
        controller.Move(move * speed * Time.deltaTime);

        animator.SetBool("Idle",false);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W))
        {
            
            animator.SetBool("Idle",false);
            animator.SetBool("isWalking",true);
            
        
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Idle",true);
            animator.SetBool("isWalking",false);
            
        
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            controller.Move(move * 2f * Time.deltaTime);
        }
        
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("isJumping",true);
            animator.SetBool("isWalking",false);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (!Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("isJumping",false);
        }
        
        if (PlayerState.Instance.currentHealth <= 0)
        {
            Debug.Log("Player died from damage!");
            
            Dead();
        }
        

 
        velocity.y += gravity * Time.deltaTime;
 
        controller.Move(velocity * Time.deltaTime);

    }

    void PlaySound(){

            WalkingSound.Play();


    }
    void Shoot() {
        // Create a new bullet instance
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
        
        // Set the bullet's velocity to be in the forward direction of the player
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        Destroy(bullet, 5f);
    }
    
    public void TakeDamage(int damageAmount, Vector3 enemyDirection)
    {
        // Reduce the player's health by the damage amount
        PlayerState.Instance.currentHealth -= damageAmount;

        // Apply a force to the player in the opposite direction of the enemy's movement
        Vector3 force = -enemyDirection.normalized * staggerForce;
        controller.Move(force * Time.deltaTime);

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Bullet"))
        {
            Destroy(hit.gameObject);
            PlayerState.Instance.currentHealth -= 1 ;
        }
        if (hit.gameObject.CompareTag("BossBullet"))
        {
            Destroy(hit.gameObject);
            PlayerState.Instance.currentHealth -= 10 ;
        }
    }



    void Dead(){
        Debug.Log("Player died!");
        Debug.Log("Respawn point position: " + respawnPoint.transform.position);
        controller.enabled = false;
        transform.position = respawnPoint.transform.position;
        PlayerState.Instance.currentHealth =  PlayerState.Instance.maxHealth;
        PlayerState.Instance.currentCalories =  PlayerState.Instance.maxCalories;
        PlayerState.Instance.currentHydrationPercent =  PlayerState.Instance.maxHydrationPercent;
        controller.enabled = true;
    }

}
