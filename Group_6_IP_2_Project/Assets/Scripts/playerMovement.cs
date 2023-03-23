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
    public GameObject Datk;
    public GameObject Satk;

    //Attack Inputs
    public KeyCode light;
    public KeyCode heavy;
    public KeyCode down;
    public KeyCode super;

    //Attack Forces 
    private float datkForce; // Force applied to character when using downwards swing 

    [SerializeField] private bool canAttack = true;

    // direction booland trigger object
    public bool facing;
    public GameObject trigger;

    //Turn round Key input
    public KeyCode turn;

    // pickup tag and Input
    public bool grenade = false;
    public KeyCode throwable;

    // Throwable Variables
    public GameObject spawner;
    public GameObject grenadethrowble;
    float thrust = -1000;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveSpeed = 0.95f;
        jumpForce = 10f;
        isGrounded = false;
        datkForce = 50f;
    }

    // A and D left right movement 
    // S change direction
    // W Jump
    // H light attack
    // J heavy Attack
    void Update()
    {
        // Old Code 
        //moveHorizontal = Input.GetAxisRaw("Horizontal"); // Sets input variables for horizontal and Vertical
        //moveVertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(light) && canAttack)
        {
            lightAttack(); 
        }

        if (Input.GetKeyDown(heavy) && canAttack)
        {
            heavyAttack();
        }

        if(Input.GetKeyDown(down) && canAttack)
        {
            downAttack();
        }

        if (Input.GetKeyDown(super))
        {
            SuperAttack();
        }

        if (Input.GetKeyDown(turn))
        {
            transform.Rotate(Vector3.up * 180);
            facing = !facing;
        }

        if (Input.GetKeyDown(throwable)) 
        {
            if (grenade) 
            {
                throwGrenade();
                grenade = false;
            }
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
        if (collison.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void lightAttack()
    {
        StartCoroutine(lAttack());
    }

    private void heavyAttack()
    {
        StartCoroutine(hAttack());

    }

    private void downAttack()
    {
        StartCoroutine(dAttack());
    }

    private void SuperAttack()
    {
        StartCoroutine(sAttack());
    }

    private IEnumerator lAttack()
    {
        canAttack = false;
        Latk.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Latk.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        canAttack = true;
    }

    private IEnumerator hAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.3f);
        Hatk.SetActive(true);
        yield return new WaitForSeconds(0.1f); 
        Hatk.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        canAttack = true;

    }

    private IEnumerator dAttack()
    {
        if(isGrounded == false)
        {
            canAttack = false;
            yield return new WaitForSeconds(0.2f);
            rb.AddForce(new Vector2(0f, -datkForce), ForceMode.Impulse);
            Datk.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            Datk.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            canAttack = true;
        }

    }

    private IEnumerator sAttack()
    {
        playerEnergy pe = GetComponent<playerEnergy>();
        if (pe.activeSuper == true)
        {
            canAttack = false;
            yield return new WaitForSeconds(0.5f);
            Satk.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Satk.SetActive(false);
            pe.EnergyReset();
            yield return new WaitForSeconds(0.2f);
            canAttack = true;
        }

    }

    private IEnumerator coolDown() 
    {
        yield return new WaitForSeconds(0.5f);
    }

    public void throwGrenade() // function to spawn a grnade and throw it
    {

        var grenade = Instantiate(grenadethrowble, new Vector3(spawner.transform.position.x, spawner.transform.position.y, spawner.transform.position.z), transform.rotation); //sets the spawn location to the rocket spawner

        grenade.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * thrust); //adds force to the rockets ridgid body
    }
}
