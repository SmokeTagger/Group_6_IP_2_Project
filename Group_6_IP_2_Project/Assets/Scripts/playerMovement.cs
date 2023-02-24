using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components 
    public Rigidbody rb; // grabs Rigidbody component from object 

    //Movement
    private float moveSpeed; 

    //Jumping
    private float jumpForce;
    public bool isGrounded; // checks to see if the object is grounded on the floor 

    //Direction
    private float moveHorizontal;
    private float moveVertical;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveSpeed = 0.75f;
        jumpForce = 5.5f;
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal"); // Sets input variables for horizontal and Vertical
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f) // if button or joystick are used if the interaction is greater than 0.1 or -0,1
        {
            rb.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode.Impulse); // applies a force to the direction of the button pressed or joystick moved 
        }

        if (isGrounded && moveVertical > 0.1f )
        {
            rb.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode.Impulse); // adds upwards force 
        }

        // Add a wall slide and wall jump 

        // Add a dash 
    }

    private void OnTriggerEnter(Collider collision) // detects collision
    {
        if(collision.gameObject.tag == "Ground") // if player comes into contact with a objects 
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider collison) // if object does not collide with ground tag stops character from jumping 
    {
        isGrounded = false;
    }
}
