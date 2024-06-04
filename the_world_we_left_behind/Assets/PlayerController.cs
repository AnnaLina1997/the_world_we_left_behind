using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject playerObj;
    public Rigidbody rb;

    public float moveSpeed;

    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        // controlled via the WAD keys
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        // left right and jump
        if (Mathf.Abs( rb.velocity.x) < 8 ) 
        {
            rb.AddRelativeForce(Vector3.right * xInput * Time.deltaTime * moveSpeed);
            
        }
        
        if (Input.GetButtonDown("Jump") && grounded == true)
        {
           rb.AddRelativeForce(Vector3.up * 1500);
        }

        // jump once per touch of the ground
        if (!grounded)
        {
            rb.AddRelativeForce(Vector3.down * 1500 * Time.deltaTime);
        }
        
        // Rotate the player to face the direction of movement
        RotatePlayer(xInput);
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        grounded = true;
    }

    private void RotatePlayer(float xInput)
    {
        if (xInput != 0)
        {
            // Determine the direction to face based on input
            float targetYRotation = xInput > 0 ? 0 : 180;
            Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);

            // Smoothly rotate the player to the target rotation
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, Time.deltaTime * 10);
        }
    }
}