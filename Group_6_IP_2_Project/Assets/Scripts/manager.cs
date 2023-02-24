using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public bool paused;

    public Slider fovSlider; // variables for fov slider
    public Camera cam;
    public Text lifeLeft; //Varaibles for the life meater
    public int lifeNumber = 100; 
    public GameObject player;
    public GameObject gameHud;
    public GameObject pauseHud;
    public GameObject overHud;
    public GameObject canvas;

    public Text hitCounter; // varaibles used fot the hit counter and ammo accsesed buy weppon scripts
    public Text ammo;
    public int hitNumber = 0;
    public int ammoLeft;
    public int ammoFull;

    public GameObject rifleObj; // varaibls to control weather a weapon has been picked op and weather it is active
    public GameObject pistolObj;
    public GameObject launcherObj;

    public bool rifleOn = false;
    public bool pistolOn = false;
    public bool launcherOn = false;

    public Text nameIn; // vraibale to hold text from the pause menu into the game
    public Text nameOut;


    void Start()
    {
        lifeLeft.text = lifeNumber + " %"; // sets the initial amount of life from the lifeNumber value 

        pauseHud.gameObject.SetActive(false);
       // overHud.gameObject.SetActive(false);
        paused = false;
        cam.fieldOfView = fovSlider.value; // set the fov slider to the camera when the game starts 

        canvas.gameObject.SetActive(true); // sets the canvas active so i can have it of in the editor and not get in the way
    }

    void Update()
    {

        nameOut.text = "Player Name : " + nameIn.text; //takes the text the player input in the pausemenu and updates it into the text feild in the game hud 

        ammo.text = "Ammo " + ammoLeft + "/" + ammoFull; // writes the values for the curently active weapon to the ammo counter
        hitCounter.text = "You have Hit Enemies " + hitNumber + " times."; //write the text for the hit counter

        if (Input.GetKeyDown(KeyCode.P) && !paused) // set the time scale to 0 i.e. paused when the game is not paused and the p key is pressed and flips the paused bool for the next statment
        {
            Time.timeScale = 0; // sets time scalle to 0 stoping the game and flips the paused bool
            paused = !paused;

            Cursor.lockState = CursorLockMode.None; // enables the cursor when paused
            pauseHud.gameObject.SetActive(true); // activates Pause Menu Hud
            gameHud.gameObject.SetActive(false);// deactivates in game hud
            return;
        }

        if(Input.GetKeyDown(KeyCode.P) && paused) // sets the time scale to 1 i.e. unpaused  when the game is paused and the p key is pressed, flips the paused bool so the first statemtn cna run again
        {
            Time.timeScale = 1; // restarts the game and flips the bool again
            paused = !paused;

            Cursor.lockState = CursorLockMode.Locked; //disables the cursor when un paused
            pauseHud.gameObject.SetActive(false); // deactivates pause menu HUD
            gameHud.gameObject.SetActive(true); // activate in Game Hud
        }

        cam.fieldOfView = fovSlider.value; // upadete the fov sider to the camera after pausing/unpausing

        /// these statments set witch weapons is active

        if (Input.GetKeyDown(KeyCode.Alpha1) && rifleOn) //checks if the keyboard 1 is pressed and if the rifle on bool is active (this is activated by the rifle pick up on colition)
        {
            rifleObj.gameObject.SetActive(true); //activates the rifle object and deactivates the other weapon objects
            pistolObj.gameObject.SetActive(false);
            launcherObj.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && pistolOn) // executes the same but for the pistol
        {
            rifleObj.gameObject.SetActive(false);
            pistolObj.gameObject.SetActive(true);
            launcherObj.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && launcherOn) // executes the same but for the launcher
        {
            rifleObj.gameObject.SetActive(false);
            pistolObj.gameObject.SetActive(false);
            launcherObj.gameObject.SetActive(true);
        }

       
    }

    public void LowerHealth() // a function that can be called to decerease the health of the player and do something when that number hits 0
    {
        lifeNumber -= 20; // decreases the lifemeter
        lifeLeft.text = "Health at " + lifeNumber + " %"; //reads the new value to the interface

        if(lifeNumber == 0) //dose something when dead
        {
            Time.timeScale = 0; //pauses the in game time
            overHud.gameObject.SetActive(true); // activates the game over screen
            gameHud.gameObject.SetActive(false); //deactivates the game Hud

        }
    }
}
