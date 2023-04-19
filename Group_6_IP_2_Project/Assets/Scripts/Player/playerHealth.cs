using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int health = 120;
    public TextMeshProUGUI healthmeter;
    public GameObject player;

    public GameObject H120;
    public GameObject H100;
    public GameObject H80;
    public GameObject H60;
    public GameObject H40;
    public GameObject H20;

    void Start()
    {
        healthmeter.text = player.name + " Health : " + health;
    }

    private void Update()
    {
        if (health == 120) 
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
        if (health <= 0)
        {
            Destroy(gameObject);    
        }


    } 

    public void healthlite()
    {
        health -= 20;
    } 

    public void healthhvy()

    {
        health -= 40;
    }

    public void healthdown()
    {
        health -= 20;
    }

    public void healthSuper()
    {
        health -= 80;
    }
}
