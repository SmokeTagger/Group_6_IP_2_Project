using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int health = 120;
    public TextMeshProUGUI healthmeter; // grabs the UI text element 
    public GameObject player; // grabs player object 
    playerMovement pM;

    public GameObject H120; // These are the diffrent segments of health 
    public GameObject H100;
    public GameObject H80;
    public GameObject H60;
    public GameObject H40;
    public GameObject H20;

    // End screen game objects
    public GameObject gameOver; 
    public GameObject winner;

    // music game objects
    public GameObject musicBox;
    public menuMusic mM;
    void Start()
    {
        healthmeter.text = player.name + " Health : " + health; // old code

    }
    

    private void Update()
    {
        musicBox = GameObject.FindWithTag("Music");
        mM = musicBox.GetComponent<menuMusic>();
        pM = player.GetComponent<playerMovement>();

        // this segment of code controls what parts of the health bar appear and what ones are disabled 
        if (health == 120) // these are controlled by settign each stage from true to false
        {
            H120.SetActive(true);
            H100.SetActive(false);
            H80.SetActive(false);
            H60.SetActive(false);
            H40.SetActive(false);
            H20.SetActive(false);
        }
        if (health == 100)
        {
            H120.SetActive(false);
            H100.SetActive(true);
            H80.SetActive(false);
            H60.SetActive(false);
            H40.SetActive(false);
            H20.SetActive(false);
        }
        if (health == 80)
        {
            H120.SetActive(false);
            H100.SetActive(false);
            H80.SetActive(true);
            H60.SetActive(false);
            H40.SetActive(false);
            H20.SetActive(false);
        }
        if (health == 60)
        {
            H120.SetActive(false);
            H100.SetActive(false);
            H80.SetActive(false);
            H60.SetActive(true);
            H40.SetActive(false);
            H20.SetActive(false);
        }
        if (health == 40)
        {
            H120.SetActive(false);
            H100.SetActive(false);
            H80.SetActive(false);
            H60.SetActive(false);
            H40.SetActive(true);
            H20.SetActive(false);
        }
        if (health == 20)
        {
            H120.SetActive(false);
            H100.SetActive(false);
            H80.SetActive(false);
            H60.SetActive(false);
            H40.SetActive(false);
            H20.SetActive(true);
        }
        if (health <= 0) // if their is no health on the player
        {
            Destroy(gameObject); // destoryed the assigned hame object
            Time.timeScale = 0; // set time to 0
            gameOver.SetActive(true); // activate the game over screen
            winner.SetActive(true);
        }


    } 

    // these code segments control what attakcks are played and how much damage each attack does 
    public void healthlite()
    {
        health -= 20;

        if (!pM.wizard)
        {
            mM.PlayWizardLight();
        }
        else
        {
            mM.PlayFighterLight();
        }
    } 

    public void healthhvy()

    {
        health -= 40;

        if (!pM.wizard)
        {
            mM.PlayWizardHeavy();
        }
        else
        {
            mM.PlayFighterHeavy();
        }
    }

    public void healthdown()
    {
        health -= 20;

        if (!pM.wizard) 
        {
            mM.PlayWizardLight();
        }
        else 
        {
            mM.PlayFighterLight();
        }
    }

    public void healthSuper()
    {
        health -= 80;
    }
}
