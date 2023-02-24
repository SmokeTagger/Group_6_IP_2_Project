using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
//using static Unity.VisualScripting.Round<TInput, TOutput>;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller; // varaibles and refernces for player movement and gravity
    float speed = 10f;

    Vector3 velocity; 
    float gravity = -10f;

    public Transform groundCheck; // varaibles for chcking if were on the groun
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public float jump = 5f; // varaible for chaning out velocity when jumping

    float packAccel = 0f; // starting jetpack acceleration
    float packMax = 0.9f; // maximum jetpack acceleration

    float fule = 100f; // starting fule
    float jetCool = 0f; //starting cooldwon to refill fule
    
    public Text fuleLeft; // text to show how much fule is left to the player

    bool moving; // bool to store if were moving

    audioManager audioScript;

    private void Awake()
    {
        audioScript = GameObject.FindGameObjectWithTag("AudioManagerTag").GetComponent<audioManager>(); // gets the audiomanager script form the audio manager and assigns it
    }
    void Start()
    {
        controller = GetComponent<CharacterController>(); // assing the charachter controler form the object with script is attached to
    }

    void Update()
    {
        int fuleInt = (int)Math.Round(fule); // converts the fule float to an integer 
        fuleLeft.text = "Fule " + fuleInt + " %"; //write the fule integer to the text feild 

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // returns true if the ground check position(player position) is withing the ground distance radius of the groundmask (witch is a layer in the editor)  

        if (isGrounded && velocity.y < 0 && !Input.GetKey(KeyCode.C)) // checks if were on the ground, were not moving up and were not pressing c
        {
            velocity.y = -2f; // is so it sets the y value of the velocity Vector 3 to -2 so we have gavity
        }

        if (Input.GetButtonDown("Jump") && isGrounded) // checks if the jump key is pressed and if were on the ground
        {
            velocity.y = jump; // if so it set the y value of the velocity vector 3 to the jump value
        }
        float x = Input.GetAxis("Horizontal"); // assing the horizontal axis and vertival axis to float x and z
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; // creates a new vector 3 from the x and z axis by multiplying the x and y transfoms by the x and z floats

        controller.Move(move * speed * Time.deltaTime); // applies this vector 3 mulitplied by the speed indipendant of frame rate to the charachter controler

        velocity.y += gravity * Time.deltaTime; // adds the gravity varaible vecotr 3 velocitys y varaible  indipendant of frame rate
        controller.Move(velocity * Time.deltaTime); // applies the velority to the charachter controler indipendant of frame rate


        if (Input.GetKey(KeyCode.C) && fule > 0) //control for the jetpack
        {
            jetCool = 0f; // resetes the refule cooldown when you start the jstpack

            if (packAccel < packMax) //code to run if the jet pack is not at max acceleration
            {
                packAccel += 0.5f * Time.deltaTime; //increase the acceleration by .5 every second
                velocity.y = 6f * packAccel;
                LooseFule(); //calls code to loose fule
            }
            else if (packAccel > packMax) //code to run if the jet pack is at max acceleration
            {
                packAccel = packMax + 0.1f; //set the acceleration to hold at just over max once max aceleration in this case to 1f as the max is 0.9
                velocity.y = 6f * packAccel;
                LooseFule();
            }

        }
        else if(fule != 100)
        {
            packAccel = 0f; //resets the acceleration when jetpack not being used
            GainFule(); // start refuling the jet pack when not being used
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) // ifstatment to controle the moving bool
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (!moving) // plays the running sound when the moving bool is false 
        {
            audioScript.PlayRunning();
        }
        if (!isGrounded) // stops the running sound if were not on the ground
        {
            audioScript.StopRunning();
        }

    }
    void LooseFule() // code to decrease the amount of fule
    {
        if (fule > 0)
        {
            fule -= 20f * Time.deltaTime; //deracese fule value by 30 every second
        }
        else if (fule < 0)
        {
            fule = 0; // sets the fule to 0 if it is less than 0
        }
    }

    void GainFule() // code to replenish fule
    {
        if (jetCool < 1.5) // check id the cool down timer is less than 1.5 and increases it by the time past
        {
            jetCool += Time.deltaTime;
        }

        else if (jetCool >= 1.5) //when the cool down timer is over 1.5 it stops increasing and start refuling
        {
            if (fule < 100) // if the fule is less that 100 it increase the full
            {
                fule += 15f * Time.deltaTime;
            }
            else if (fule > 100f) // if the fule is over 100 it set it to 100
            {
                fule = 100f;
            }
        }
    }
}
