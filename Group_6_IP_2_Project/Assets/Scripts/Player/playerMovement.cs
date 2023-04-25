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

    public GameObject baseModle;

    //Attacks
    public GameObject Latk;
    public GameObject Hatk1;
    public GameObject Hatk2;
    public GameObject Datk;
    public GameObject Satk;
    public GameObject SatkMarker;

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
    float thrust = 1000;
    public GameObject grenadeMarker;

    public GameObject musicBox;
    menuMusic mM;
    public bool wizard;

    float jumptime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();

        moveSpeed = 0.95f;
        jumpForce = 10f;
        isGrounded = false;
        datkForce = 50f;

        if(!wizard) { thrust = -1000; }
    }

    // A and D left right movement 
    // S change direction
    // W Jump
    // H light attack
    // J heavy Attack
    void Update()
    {
        musicBox = GameObject.FindWithTag("Music"); // grabs the audio object tagged music 
        mM = musicBox.GetComponent<menuMusic>(); // then grabs the menu music component assigned to the object 

        jumptime += Time.deltaTime; // keeps the timer for the jump reset contained to the real time in game 

        if(jumptime > 2) // if the jump time is greater than 2 seconds 
        { 
            isGrounded = true; // sets the grounded variabnle to true allowing jumping 
            jumptime = 0;
        }

        if (Input.GetKeyDown(light) && canAttack) // if assigned attack button is pressed the character will attack if variable can attack is true
        {
            lightAttack(); // activates the assigned attack function 

            if(wizard) // plays diffrent animation depending on which character the player is using
            { 
                mM.PlayWizardSwing(); 
            }
            else 
            { 
                mM.PlayFighterSwing(); 
            }
        }

        if (Input.GetKeyDown(heavy) && canAttack) // if assigned attack button is pressed the character will attack if variable can attack is true
        {
            heavyAttack(); // activates the assigned attack function

            if (wizard) // plays diffrent animation depending on which character the player is using
            {
                mM.PlayWizardSwing();
            }
            else
            {
                mM.PlayFighterSwing();
            }
        }

        if(Input.GetKeyDown(down) && canAttack) // if assigned attack button is pressed the character will attack if variable can attack is true
        {
            downAttack(); // activates the assigned attack function

            if (wizard) // plays diffrent animation depending on which character the player is using
            {
                mM.PlayWizardSwing(); 
            }
            else
            {
                mM.PlayFighterSwing();
            }
        }

        if (Input.GetKeyDown(super))
        {
            SuperAttack();
            canAttack = true;
        }

        if (Input.GetKeyDown(turn)) // this functions allows the character to be manually turned around 
        {
            transform.Rotate(Vector3.up * 180);
            facing = !facing; // keeps track of where the character is facing for the hit boxes 
        }

        if (Input.GetKeyDown(throwable)) 
        {
            if (grenade) 
            {
                throwGrenade();
                grenade = false; // disables the ability to thrown a granade 

                if (wizard)
                {
                    mM.PlayWizardSwing();
                }
                else
                {
                    mM.PlayFighterSwing();
                }
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

        if (grenade) // this us used to activate the Granade indicator to let the player know if they are holding a granade 
        { 
            grenadeMarker.SetActive(true);
        }
        else 
        {
            grenadeMarker.SetActive(false);
        }

        playerEnergy pe = GetComponent<playerEnergy>(); // grabs energy component 
        if (pe.activeSuper == true) 
        { 
            SatkMarker.SetActive(true); // lets player know if they have the energy to activate the super attack
        }
        else 
        {
            SatkMarker.SetActive(false);
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
            if (wizard)
            {
                mM.PlayWizardJump();
            }
            else
            {
                mM.PlayFighterJump();
            }

        }

    }

   private void OnCollisionEnter(Collision col) // detects collision
    {
        if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Destructable") // if player comes into contact with a objects 
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision col) // if object does not collide with ground tag stops character from jumping 
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Destructable")
        {
            isGrounded = false;
        }
    }

    // this list of method are used to activate the assigned Coroutines for all the attacks 
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
        canAttack = false; // sets the players ability to attack to false 
        baseModle.SetActive(false);
        Latk.SetActive(true); // allowing the animation to appear 
        yield return new WaitForSeconds(0.1f); // this is used to lengthen the attack to allow animations to play 
        Latk.SetActive(false);
        baseModle.SetActive(true) ;
        yield return new WaitForSeconds(0.2f); // waits a moment before allowing the player to attack again 
        canAttack = true; // rsets the players abilitiy to attack
    }

    private IEnumerator hAttack()
    {
        canAttack = false;
        baseModle.SetActive(false);
        Hatk1.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Hatk1.SetActive(false);
        Hatk2.SetActive(true);
        yield return new WaitForSeconds(0.1f); 
        Hatk2.SetActive(false);
        baseModle.SetActive(true);
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
            baseModle.SetActive(false);
            Datk.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            Datk.SetActive(false);
            baseModle.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            canAttack = true;
        }

    }

    private IEnumerator sAttack()
    {
        playerEnergy pe = GetComponent<playerEnergy>();
        if (pe.activeSuper == true) // the active super variable must be true to allow this attack to start 
        {
            canAttack = false;
            yield return new WaitForSeconds(0.5f);
            baseModle.SetActive(false);
            Satk.SetActive(true);
            mM.PlaySuper();
            yield return new WaitForSeconds(0.5f);
            Satk.SetActive(false);
            baseModle.SetActive(true) ;
            pe.EnergyReset(); // sets the energy of the attack to zero 
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
