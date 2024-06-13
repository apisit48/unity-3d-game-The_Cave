using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 5f;
    public float lineOfSight = 10f;
    public float stoppingDistance = 0.5f;
    public float gravity = 9.81f;

    public float health = 100;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;

    private Transform player;

    public Animator animator;

    private CharacterController controller;
    bool isGrounded;
    public AudioSource deadSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        controller = GetComponent<CharacterController>();

    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        animator.SetBool("Idle",true);
        animator.SetBool("isRunning",false);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        // Check if player is within line of sight
        if (Vector3.Distance(transform.position, player.position) <= lineOfSight)
        {
            // Look at player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // Move towards player if distance is greater than stopping distance
            if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
            {
                animator.SetBool("Idle",false);
                animator.SetBool("isRunning",true);
                Vector3 movement = direction * moveSpeed * Time.deltaTime;
                movement.y -= gravity * Time.deltaTime;
                controller.Move(movement);
            }

        }
        if (health <= 0)
        {
            deadSound.Play();
            Destroy(gameObject);
        }
    }



    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(hit.gameObject);
            health -= 1 ;
            Debug.Log("Enemy health: " + health);
        }
        if (hit.gameObject.CompareTag("Player"))
        {
            health = 0  ;
            Debug.Log("Enemy health: " + health);
            PlayerState.Instance.currentHealth -= 10;
        }
    }

}

