using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public CharacterController controller;  // Store the CharacterController component
    private Vector2 moveDirection;          // only used a two dimensional vector here because i only need to move the player in the x and y axis

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController component
    }

    // Update is called once per frame
    void Update()
    {

        moveDirection = new Vector2(Input.GetAxis("Horizontal") * (moveSpeed / 2), moveDirection.y);  // Get the horizontal input and move the player in the air
        if (controller.isGrounded)  // Check if the player is on the ground
        {
            moveDirection = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y);  // Get the horizontal input and move the player

            if (Input.GetButtonDown("Jump"))    // Check if the player pressed the jump button
            {
                moveDirection.y = jumpForce;    // Apply the jump force to the player

            }
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime * gravityScale);    // Apply gravity to the player
        controller.Move(moveDirection * Time.deltaTime);    // Move the player
        RotatePlayer(Input.GetAxis("Horizontal"));    // Rotate the player
    }

    // Rotate the player based on input
    private void RotatePlayer(float xInput)
    {
        if (xInput != 0)
        {
            // Determine the direction to face based on input
            float targetYRotation = xInput > 0 ? 0 : -180;
            Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);

            // Smoothly rotate the player to the target rotation
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, targetRotation, Time.deltaTime * 10);
        }
    }
}