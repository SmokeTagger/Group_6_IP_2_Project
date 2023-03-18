using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //Components 
    public Rigidbody rb; // grabs Rigidbody component from object 

    //Movement
    private float moveSpeed;

    //Jumping
    private float jumpForce;
    public bool isGrounded; // checks to see if the object is grounded on the floor 

    //Direction
    public KeyCode left; // These are used to set the controls for the character in the inspector 
    public KeyCode right;
    public KeyCode jump;
    //private float moveHorizontal;
    //private float moveVertical;

    //Attacks
    public GameObject Latk;
    public GameObject Hatk;

    //Attack Inputs
    public KeyCode light;
    public KeyCode heavy;

    // direction booland trigger object
    [SerializeField] bool facing = true;
    public GameObject trigger;

    //Turn round Key input
    public KeyCode turn;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveSpeed = 0.95f;
        jumpForce = 18.5f;
        isGrounded = true;
    }

    // A and D left right movement 
    // S change direction
    // W Jump
    // H light attack
    // J heavy Attack
    void Update()
    {
        //moveHorizontal = Input.GetAxisRaw("Horizontal"); // Sets input variables for horizontal and Vertical
        //moveVertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(light))
        {
            lightAttack(); 
        }

        if (Input.GetKey(heavy))
        {
            heavyAttack();
        }

        if(Input.GetKey(turn))
        {
            transform.Rotate(Vector3.up * 180);
            facing = !facing;
        }

        if (facing)
        {
            trigger.SetActive(true);
        }

        if (!facing)
        {
            trigger.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(left))
        {
            rb.AddForce(new Vector2(/*moveHorizontal **/ -moveSpeed, 0f), ForceMode.Impulse); // applies a force to the direction of the button pressed or joystick moved 
        }

        if (Input.GetKey(right))
        {
            rb.AddForce(new Vector2(/*moveHorizontal **/ moveSpeed, 0f), ForceMode.Impulse); // applies a force to the direction of the button pressed or joystick moved 
        }

        if (isGrounded && Input.GetKey(jump))
        {
            rb.AddForce(new Vector2(0f,/* moveVertical **/ jumpForce), ForceMode.Impulse); // adds upwards force 
        }

        // Add a wall slide and wall jump 

        // Add a dash 
    }

    private void OnCollisionEnter(Collision collision) // detects collision
    {
        if(collision.gameObject.tag == "Ground") // if player comes into contact with a objects 
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collison) // if object does not collide with ground tag stops character from jumping 
    {
        isGrounded = false; 
    }

    private void lightAttack()
    {
        StartCoroutine(lAttack());
    }

    private void heavyAttack()
    {
        StartCoroutine(hAttack());
    }

    private IEnumerator lAttack()
    {
        Latk.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Latk.SetActive(false);
    }

    private IEnumerator hAttack()
    {
        yield return new WaitForSeconds(0.3f);
        Hatk.SetActive(true);
        yield return new WaitForSeconds(0.1f); 
        Hatk.SetActive(false);

    }
}
