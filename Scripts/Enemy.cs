using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 5f;
    public float lineOfSight = 10f;
    public float stoppingDistance = 2f;
    public GameObject projectilePrefab;
    public float shootingRate = 1f;
    public float projectileSpeed = 10f;
    public float gravity = 9.81f;

    public float health = 100;

    private float nextShotTime;
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
        animator.SetBool("isWalking",false);
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
                animator.SetBool("isWalking",true);
                Vector3 movement = direction * moveSpeed * Time.deltaTime;
                movement.y -= gravity * Time.deltaTime;
                controller.Move(movement);
            }

            // Shoot at player
            if (Time.time >= nextShotTime)
            {
                Shoot();
                nextShotTime = Time.time + 1f / shootingRate;
            }
        }
        if (health <= 0)
        {
            deadSound.Play();
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 spawnPosition = transform.position + direction * 1.5f;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
        Destroy(projectile, 3f); // Destroy the projectile after 5 seconds
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Weapon"))
        {
            health -= 2 ;
            Debug.Log("Enemy health: " + health);
        }
        if (hit.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(hit.gameObject);
            health -= 1 ;
            Debug.Log("Enemy health: " + health);
        }
    }

}

