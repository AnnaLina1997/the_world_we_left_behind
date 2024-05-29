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
}