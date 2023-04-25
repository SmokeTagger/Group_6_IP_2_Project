using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MenuManager : MonoBehaviour
{
    //reference oibjects to keep track of level selection
    public GameObject level1;
    public GameObject level2;

    //UI Canvas objects
    public GameObject characterSelect;
    public GameObject hud;
    public GameObject pauseMenu;
    
    // reference objects to keep track of with chrachters the player choose
    public GameObject wizard1;
    public GameObject wizard2;
    public GameObject fighter1;
    public GameObject fighter2;

    //spawn points for the charachters 
    public GameObject player1Spawn;
    public GameObject player2Spawn;

    //charachter prefab objects
    public GameObject wizardPrefab;
    public GameObject fighterPrefab;

    //the camerea
    public GameObject multiCam;

    // UI elements for  player 1 health
    public GameObject P1H120;
    public GameObject P1H100;
    public GameObject P1H80;
    public GameObject P1H60;
    public GameObject P1H40;
    public GameObject P1H20;

    // UI Elements for player 2 health
    public GameObject P2H120;
    public GameObject P2H100;
    public GameObject P2H80;
    public GameObject P2H60;
    public GameObject P2H40;
    public GameObject P2H20;

    // Ui elements for player 1 and 2 grenade and super move
    public GameObject P1Grenade;
    public GameObject P2Grenade;
    public GameObject P1Super;
    public GameObject P2Super;


    // UI Caanvas objects for the game over screen
    public GameObject GameOver;
    public GameObject winner1;
    public GameObject winner2;

    // variable sot store the music box and music manager
    public GameObject musicBox;
    public menuMusic mM;

    //stop the in game time when the game launches
    public void Start()
    {
        Time.timeScale = 0;
        

    }

    // grap the music box and manager from it
    public void Update()
    {
        musicBox = GameObject.FindWithTag("Music");
        mM = musicBox.GetComponent<menuMusic>();


        //activates the pause screen and stops the ingame time when p is pressed
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0; 
        }
    }

    // resets the game time when the pause menu is closed
    public void Continue() 
    {
        Time.timeScale = 1;
    }

    // check the reference for witch level was slected and loads teh asociated scene
    public void StartGame() 
    {
        if (level1.activeInHierarchy || level2.activeInHierarchy) 
        {
            if (level1.activeInHierarchy)
            {
                SceneManager.LoadScene(1);
            }
            else if (level2.activeInHierarchy) 
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    // switches out the music and loads the corect scene when any of the main menu buttons are pressed
    public void MainMenu()
    {
        Time.timeScale = 0;
        if (musicBox != null) 
        { 
        mM.stopBattle();
        mM.playBackground();
        }
        SceneManager.LoadScene(0);
    }

    // closed the program when called
    public void Quit()
    {
        Application.Quit();
    }

    // checks if both players have selected a charachter before continuing
    public void StartLevel()
    {
        if (wizard1.activeInHierarchy || fighter1.activeInHierarchy) 
        {
            if(wizard2.activeInHierarchy || fighter2.activeInHierarchy) 
            {
                //if both players have character, it starts the in game time, activates the in game hud and deactivats the select screen
                Time.timeScale = 1;
                hud.SetActive(true);
                characterSelect.SetActive(false);
                
                // spawns in the wizzard for player 1 and assisnges the relevent values to the player movement and player health scripts
                // also adds the new wizzard character to the camera list to keep track of its locaton
                if (wizard1.activeInHierarchy) 
                {
                    var tospawn = Instantiate(wizardPrefab, player1Spawn.transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                    playerMovement script = tospawn.GetComponent<playerMovement>();
                    playerHealth health = tospawn.GetComponent<playerHealth>();
                    script.left = KeyCode.A;
                    script.right = KeyCode.D;
                    script.jump = KeyCode.W;
                    script.light = KeyCode.T;
                    script.heavy = KeyCode.U;
                    script.down = KeyCode.Y;
                    script.super = KeyCode.F;
                    script.turn = KeyCode.S;
                    script.throwable = KeyCode.R;
                    script.facing = true;
                    script.SatkMarker = P1Super;
                    script.grenadeMarker = P1Grenade;
                    script.wizard = true;
                    health.H120 = P1H120;
                    health.H100 = P1H100;
                    health.H80 = P1H80;
                    health.H60 = P1H60;
                    health.H40 = P1H40;
                    health.H20 = P1H20;
                    health.gameOver = GameOver;
                    health.winner = winner2;
                    MultipleTargetCamera mtc = multiCam.GetComponent<MultipleTargetCamera>();
                    mtc.target.Add(tospawn.transform);
                }

                //same as above but for player 2
                if (wizard2.activeInHierarchy)
                {
                    var tospawn = Instantiate(wizardPrefab, player2Spawn.transform.position, player1Spawn.transform.rotation);
                    playerMovement script = tospawn.GetComponent<playerMovement>();
                    playerHealth health = tospawn.GetComponent<playerHealth>();
                    script.left = KeyCode.LeftArrow;
                    script.right = KeyCode.RightArrow;
                    script.jump = KeyCode.UpArrow;
                    script.light = KeyCode.Keypad1;
                    script.heavy = KeyCode.Keypad3;
                    script.down = KeyCode.Keypad2;
                    script.super = KeyCode.Keypad0;
                    script.turn = KeyCode.DownArrow;
                    script.throwable = KeyCode.KeypadEnter;
                    script.facing = false;
                    script.SatkMarker = P2Super;
                    script.grenadeMarker = P2Grenade;
                    script.wizard = true;
                    health.H120 = P2H120;
                    health.H100 = P2H100;
                    health.H80 = P2H80;
                    health.H60 = P2H60;
                    health.H40 = P2H40;
                    health.H20 = P2H20;
                    health.gameOver = GameOver;
                    health.winner = winner1;
                    MultipleTargetCamera mtc = multiCam.GetComponent<MultipleTargetCamera>();
                    mtc.target.Add(tospawn.transform);
                }

                // same as above but for the player 1 and fighter
                if (fighter1.activeInHierarchy)
                {
                    var tospawn = Instantiate(fighterPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
                    playerMovement script = tospawn.GetComponent<playerMovement>();
                    playerHealth health = tospawn.GetComponent<playerHealth>();
                    script.left = KeyCode.A;
                    script.right = KeyCode.D;
                    script.jump = KeyCode.W;
                    script.light = KeyCode.T;
                    script.heavy = KeyCode.U;
                    script.down = KeyCode.Y;
                    script.super = KeyCode.F;
                    script.turn = KeyCode.S;
                    script.throwable = KeyCode.R;
                    script.facing = true;
                    script.SatkMarker = P1Super;
                    script.grenadeMarker = P1Grenade;
                    script.wizard = false;
                    health.H120 = P1H120;
                    health.H100 = P1H100;
                    health.H80 = P1H80;
                    health.H60 = P1H60;
                    health.H40 = P1H40;
                    health.H20 = P1H20;
                    health.gameOver = GameOver;
                    health.winner = winner2;
                    MultipleTargetCamera mtc = multiCam.GetComponent<MultipleTargetCamera>();
                    mtc.target.Add(tospawn.transform);
                }

                //same as above but for the fighter and player 2
                if (fighter2.activeInHierarchy)
                {
                    var tospawn = Instantiate(fighterPrefab, player2Spawn.transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                    playerMovement script = tospawn.GetComponent<playerMovement>();
                    playerHealth health = tospawn.GetComponent<playerHealth>();
                    script.left = KeyCode.LeftArrow;
                    script.right = KeyCode.RightArrow;
                    script.jump = KeyCode.UpArrow;
                    script.light = KeyCode.Keypad1;
                    script.heavy = KeyCode.Keypad3;
                    script.down = KeyCode.Keypad2;
                    script.super = KeyCode.Keypad0;
                    script.turn = KeyCode.DownArrow;
                    script.throwable = KeyCode.KeypadEnter;
                    script.facing = false;
                    script.SatkMarker = P2Super;
                    script.grenadeMarker = P2Grenade;
                    script.wizard = false;
                    health.H120 = P2H120;
                    health.H100 = P2H100;
                    health.H80 = P2H80;
                    health.H60 = P2H60;
                    health.H40 = P2H40;
                    health.H20 = P2H20;
                    health.gameOver = GameOver;
                    health.winner = winner1;
                    MultipleTargetCamera mtc = multiCam.GetComponent<MultipleTargetCamera>();
                    mtc.target.Add(tospawn.transform);
                }

            }
        }
    }

}
