using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float speed = 0.05f;
    private float jumpForce = 2.5f;
    private float RollSpeed = 2f;
    [SerializeField] float RollCooldown = 2f;
    private float nextRollTime = 0f;

    public bool canBeDetected;

    private Vector3 move;
    public Rigidbody rb;
    public Animator animator;
    public Transform groundCheck;
    public LayerMask ground;
    public GameManager gm;
    public LevelLoader ll;
    public AudioSource songplayer;
    public AudioClip BattleMusic;


    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ll = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        songplayer = GameObject.Find("SongPlayer").GetComponent<AudioSource>();
        transform.position = gm.playerPosition;
        canBeDetected = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded())
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                
            }
        }

        //Roll or Dive
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            if(isGrounded())
            {
                if(Time.time > nextRollTime)
                {
                    if(Input.GetKey(KeyCode.W))
                    {
                    rb.AddForce(Vector3.forward * RollSpeed, ForceMode.Impulse);
                    }
                    else if(Input.GetKey(KeyCode.S))
                    {
                    rb.AddForce(Vector3.back * RollSpeed, ForceMode.Impulse);
                    }
                    else if(Input.GetKey(KeyCode.A))
                    {
                    rb.AddForce(Vector3.left * RollSpeed, ForceMode.Impulse);
                    }
                    else if(Input.GetKey(KeyCode.D))
                    {
                    rb.AddForce(Vector3.right * RollSpeed, ForceMode.Impulse);
                    }
                    nextRollTime = Time.time + RollCooldown;
                }
            }
            
            else
            {
                if(Input.GetKey(KeyCode.W))
                {
                    rb.AddForce(Vector3.forward * RollSpeed, ForceMode.Impulse);
                    animator.SetInteger("LastDirection", 0);
                }
                else if(Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(Vector3.back * RollSpeed, ForceMode.Impulse);
                    animator.SetInteger("LastDirection", 1);
                }
                else if(Input.GetKey(KeyCode.A))
                {
                    rb.AddForce(Vector3.left * RollSpeed, ForceMode.Impulse);
                    animator.SetInteger("LastDirection", 2);
                }
                else if(Input.GetKey(KeyCode.D))
                {
                    rb.AddForce(Vector3.right * RollSpeed, ForceMode.Impulse);
                    animator.SetInteger("LastDirection", 3);
                }
            }
        }

        //WASD Movement
        move.x = Input.GetAxisRaw("Horizontal");
        move.z = Input.GetAxisRaw("Vertical");
        //Add slow drift in air
        if(isGrounded())
        {
            rb.MovePosition(rb.position + move*speed*Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + move*speed*Time.fixedDeltaTime);
        }

        //Animator Update
        if(Input.GetKeyUp(KeyCode.W))
        {
            animator.SetInteger("LastDirection", 0);
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            animator.SetInteger("LastDirection", 1);
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            animator.SetInteger("LastDirection", 2);
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            animator.SetInteger("LastDirection", 3);
        }
        animator.SetFloat("Horizontal", move.x);
        animator.SetFloat("Vertical", move.z);
        animator.SetFloat("Speed", move.sqrMagnitude);
        

        // if(Input.GetKeyDown(KeyCode.N))
        // {
        //     SceneManager.LoadScene(2);
        // }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            canBeDetected = false;
            gm.scenetoload = SceneManager.GetActiveScene().buildIndex;
            gm.playerPosition = transform.position;
            songplayer.clip = BattleMusic;
            songplayer.Play();
            StartCoroutine(ll.LoadLevel(1));
            
        }
    }

    //Checks if the player is grounded, returns true or false.
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.2f, ground);
    }
}
