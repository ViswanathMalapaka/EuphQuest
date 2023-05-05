using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
     public float movementSpeed = 1.0f;
    public float playerDetectionRadius = 3.0f;
    public float restrictedRadius = 5.0f;
    public int ShouldSpawn;
    public Vector3 restrictedPoint;
    public GameObject player;
    public PlayerController pc;
    private Transform playerTransform;
    public Animator animator;
    public Rigidbody rb;
    private Vector3 moveVector;
    public Vector3 spawnpoint;
    public int posInList;
    public EnvironmentManager em;

    private void Start()
    {
        //Get the animator component
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        // Get the player's transform component
        player = GameObject.Find("player");
        pc = player.GetComponent<PlayerController>();
        playerTransform = player.transform;

        em = GameObject.Find("EvironmentManager").GetComponent<EnvironmentManager>();
        //Set the restricted radius
        restrictedPoint = transform.position;
    }

    private void Update()
    {

        if(pc.canBeDetected)
        {
             // Check if the player is within the detection radius
            if (Vector3.Distance(transform.position, playerTransform.position) <= playerDetectionRadius && Vector3.Distance(transform.position, restrictedPoint) < restrictedRadius)
            {
                // Move towards the player
                Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                moveVector = targetPosition - transform.position;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            }
            //move back towards the center
            else if (Vector3.Distance(transform.position, restrictedPoint) > 0) 
            {
                moveVector = restrictedPoint - transform.position;
                transform.position = Vector3.MoveTowards(transform.position, restrictedPoint, movementSpeed * 0.6f * Time.deltaTime);
            }

            //set animator values
            animator.SetFloat("Speed", moveVector.sqrMagnitude);
            if(moveVector.x > 0)
            {
                animator.SetFloat("Horizontal", 1);
            }
            else if (moveVector.x < 0)
            {
                animator.SetFloat("Horizontal", 0);
            }
        }

        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("This boy Won't Spawn Next Time");
            em.enemyList.enemies[posInList].ShouldSpawn = 0;
            
        }
    }
}
