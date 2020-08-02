using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float speed = 5f;
    [SerializeField] float gravity = 9.81f;
    
    CharacterController controller;
   
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    
    void Update()
    {
        PlayerMovement();
        
    }

    private void PlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveX, 0f, moveZ); // We add the direction the Player should move
        Vector3 velocity = direction * speed; // Here we store the direction of the movement with the velocity

        velocity.y -= gravity * Time.deltaTime; // I try to keep it consistent for my physics based characther.
        velocity = transform.transform.TransformDirection(velocity);

        controller.Move(velocity * Time.deltaTime);
    }

}
