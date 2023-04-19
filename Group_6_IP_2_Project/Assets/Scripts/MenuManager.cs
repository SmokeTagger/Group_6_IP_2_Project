using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;

    public GameObject characterSelect;
    public GameObject hud;
    public GameObject pauseMenu;

    public GameObject wizard1;
    public GameObject wizard2;
    public GameObject fighter1;
    public GameObject fighter2;

    public GameObject player1Spawn;
    public GameObject player2Spawn;

    public GameObject wizardPrefab;
    public GameObject fighterPrefab;

    public GameObject multiCam;

    public GameObject P1H120;
    public GameObject P1H100;
    public GameObject P1H80;
    public GameObject P1H60;
    public GameObject P1H40;
    public GameObject P1H20;

    public GameObject P2H120;
    public GameObject P2H100;
    public GameObject P2H80;
    public GameObject P2H60;
    public GameObject P2H40;
    public GameObject P2H20;

    public Slider levelMusic;
    public Slider levelSFX;
    public Slider LevelBrightness;

    public void Start()
    {
        Time.timeScale = 0;
        //Instantiate(wizardPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0; 
        }
    }

    public void Continue() 
    {
        Time.timeScale = 1;
    }
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
                print("Level to loading");
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartLevel()
    {
        if (wizard1.activeInHierarchy || fighter1.activeInHierarchy) 
        {
            if(wizard2.activeInHierarchy || fighter2.activeInHierarchy) 
            {
                Time.timeScale = 1;
                hud.SetActive(true);
                characterSelect.SetActive(false);
                Music();
                
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
                    health.H120 = P1H120;
                    health.H100 = P1H100;
                    health.H80 = P1H80;
                    health.H60 = P1H60;
                    health.H40 = P1H40;
                    health.H20 = P1H20;
                    MultipleTargetCamera mtc = multiCam.GetComponent<MultipleTargetCamera>();
                    mtc.target.Add(tospawn.transform);
                }

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
                    health.H120 = P2H120;
                    health.H100 = P2H100;
                    health.H80 = P2H80;
                    health.H60 = P2H60;
                    health.H40 = P2H40;
                    health.H20 = P2H20;
                    MultipleTargetCamera mtc = multiCam.GetComponent<MultipleTargetCamera>();
                    mtc.target.Add(tospawn.transform);
                }

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
                    health.H120 = P1H120;
                    health.H100 = P1H100;
                    health.H80 = P1H80;
                    health.H60 = P1H60;
                    health.H40 = P1H40;
                    health.H20 = P1H20;
                    MultipleTargetCamera mtc = multiCam.GetComponent<MultipleTargetCamera>();
                    mtc.target.Add(tospawn.transform);
                }

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
                    health.H120 = P2H120;
                    health.H100 = P2H100;
                    health.H80 = P2H80;
                    health.H60 = P2H60;
                    health.H40 = P2H40;
                    health.H20 = P2H20;
                    MultipleTargetCamera mtc = multiCam.GetComponent<MultipleTargetCamera>();
                    mtc.target.Add(tospawn.transform);
                }

            }
        }
    }

    private void Music() 
    {
        var musicBox = GameObject.FindWithTag("Music");

        if (musicBox != null)
        {
            menuMusic mM = musicBox.GetComponent<menuMusic>();
            mM.stopBackground();


            int rand = Random.Range(1, 3);

            switch (rand)
            {
                case 1:
                    mM.playBattle1();
                    break;

                case 2:
                    mM.playBattle2();
                    break;

                case 3:
                    mM.playBattle3();
                    break;
            }

            levelMusic.value = mM.musicValue;
            levelSFX.value = mM.sfxValue;
            LevelBrightness.value = mM.brightnessValue;
        }
    }
}
