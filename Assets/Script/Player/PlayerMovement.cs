using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float playerSpeed = 10f;
    [Header("Jumping")]
    [SerializeField] float jumpHeight;
    [SerializeField] float gravity = -9.81f;

    CharacterController controller;
    Vector3 velocity;
    [Header("Ground Check")]
    [SerializeField] float groundDistance = 0.5f;
    [SerializeField] Transform groundCheck;    
    [SerializeField] LayerMask groundMask;
    
    bool isGrounded;


    
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Movement();       
    }

    private void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // We make our player movement according his Vector3 position
        Vector3 direction = transform.right * moveX + transform.forward * moveZ;
        controller.Move(direction * playerSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            

        }

        // Here i apply gravity to the player.
        velocity.y += gravity * Time.deltaTime;        
        controller.Move(velocity * Time.deltaTime);        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }

}
