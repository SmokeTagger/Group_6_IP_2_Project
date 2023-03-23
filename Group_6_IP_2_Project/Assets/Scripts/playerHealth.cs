using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    [SerializeField] int health = 200;
    public TextMeshProUGUI healthmeter;
    public GameObject player;

    void Start()
    {
        healthmeter.text = player.name + " Health : " + health;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);    
        }
    } 

    public void healthlite()
    {
        health -= 10;
        healthmeter.text = player.name + " Health : " + health;
    } 

    public void healthhvy()
    {
        health -= 30;
        healthmeter.text = player.name + " Health : " + health;
    }

    public void healthdown()
    {
        health -= 20;
        healthmeter.text = player.name + " Health : " + health;
    }

    public void healthSuper()
    {
        health -= 50;
        healthmeter.text = player.name + " Health : " + health;
    }
}
