using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller; // Character controller

    public float speed = 12f; // Movement speed
    public float gravity = -9.81f; // Gravity

    public float jumpHeight = 3f; // Jump height

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity; // Velocity
    bool isGrounded; // Is the player grounded?


    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Check if the player is grounded

        if (isGrounded && velocity.y < 0) // If the player is grounded and the velocity is less than 0
        {
            velocity.y = -2f; // Set the velocity to -2
        }

        float x = Input.GetAxis("Horizontal"); // Get the horizontal axis
        float z = Input.GetAxis("Vertical"); // Get the vertical axis

        Vector3 move = transform.right * x + transform.forward * z; 
        controller.Move(move * speed *Time.deltaTime); // Move the player

        if(Input.GetButtonDown("Jump") && isGrounded) // If the player presses the jump button and is grounded
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Set the velocity to the square root of 2 * -2 * gravity
        }

        velocity.y += gravity * Time.deltaTime; // Apply gravity

        controller.Move(velocity * Time.deltaTime); // Move the player
    }
}
