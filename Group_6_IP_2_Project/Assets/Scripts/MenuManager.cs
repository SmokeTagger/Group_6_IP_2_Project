using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;

    public GameObject characterSelect;
    public GameObject hud;

    public GameObject wizard1;
    public GameObject wizard2;
    public GameObject fighter1;
    public GameObject fighter2;

    public GameObject player1Spawn;
    public GameObject player2Spawn;

    public GameObject wizardPrefab;
    public GameObject fighterPrefab;

    public void Start()
    {
        Time.timeScale = 0;
        //Instantiate(wizardPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
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

                if (wizard1.activeInHierarchy) 
                {
                    var tospawn = Instantiate(wizardPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
                    playerMovement script = tospawn.GetComponent<playerMovement>();
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
                }

                if (wizard2.activeInHierarchy)
                {
                    var tospawn = Instantiate(wizardPrefab, player2Spawn.transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                    playerMovement script = tospawn.GetComponent<playerMovement>();
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
                }

                if (fighter1.activeInHierarchy)
                {
                    var tospawn = Instantiate(fighterPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
                    playerMovement script = tospawn.GetComponent<playerMovement>();
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
                }

                if (fighter2.activeInHierarchy)
                {
                    var tospawn = Instantiate(fighterPrefab, player2Spawn.transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                    playerMovement script = tospawn.GetComponent<playerMovement>();
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
                }

            }
        }
    }
}
